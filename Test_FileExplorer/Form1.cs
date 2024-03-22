using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Test_FileExplorer
{
    public partial class MainForm : Form
    {
        private string startDirectory = string.Empty;
        private Regex fileNameRegex = new Regex(string.Empty);
        private bool searching;

        public MainForm()
        {
            InitializeComponent();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            if (!searching && (startDirectoryTextBox.Text != String.Empty))
            {
                startDirectory = startDirectoryTextBox.Text;
                fileNameRegex = new Regex(fileNameRegexTextBox.Text);
                searching = true;

                // Clear previous search results
                filesTreeView.Nodes.Clear();
                searchStatusLabel.Text = "Searching...";
                foundFilesCountLabel.Text = "";
                totalFilesCountLabel.Text = "";
                elapsedTimeLabel.Text = "";

                // Start search in a separate thread
                Thread searchThread = new Thread(SearchFiles);
                searchThread.Start();
            }
            else
            {
                searching = false;
            }
        }

        private void SearchFiles()
        {
            int foundFilesCount = 0;
            int totalFilesCount = 0;
            DateTime startTime = DateTime.Now;

            try
            {
                DirectoryInfo rootDirectory = new DirectoryInfo(startDirectory);
                TreeNode rootNode = new TreeNode();

                Invoke(new Action(() =>
                {
                    rootNode = filesTreeView.Nodes.Add(rootDirectory.FullName, rootDirectory.Name);
                }));

                Queue<TreeNode> directories = new Queue<TreeNode>();
                directories.Enqueue(rootNode);

                while (directories.Count > 0 && searching)
                {
                    TreeNode currentDirNode = directories.Dequeue();
                    DirectoryInfo currentDir = new DirectoryInfo(currentDirNode.Name);

                    try
                    {
                        foreach (var directory in currentDir.GetDirectories())
                        {
                            TreeNode dirNode = new TreeNode();

                            Invoke(new Action(() =>
                            {
                                dirNode = currentDirNode.Nodes.Add(directory.FullName, directory.Name);
                            }));

                            directories.Enqueue(dirNode);
                        }

                        foreach (var file in currentDir.GetFiles())
                        {
                            totalFilesCount++;
                            if (fileNameRegex.IsMatch(file.Name))
                            {
                                foundFilesCount++;

                                Invoke(new Action(() =>
                                {
                                    currentDirNode.Nodes.Add(file.FullName, file.Name);
                                }));
                            }
                        }

                        UpdateSearchStatus(currentDir.FullName, foundFilesCount, totalFilesCount, startTime);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        // Access to directory denied, continue with next directory
                        continue;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message);
                        searching = false;
                        break;
                    }
                }

                UpdateSearchStatus("", foundFilesCount, totalFilesCount, startTime);
            }
            finally
            {
                searching = false;
            }
        }

        private void UpdateSearchStatus(string currentDirectory, int foundFilesCount, int totalFilesCount, DateTime startTime)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => UpdateSearchStatus(currentDirectory, foundFilesCount, totalFilesCount, startTime)));
                return;
            }

            searchStatusLabel.Text = "Current Directory: " + currentDirectory;
            foundFilesCountLabel.Text = "Found Files: " + foundFilesCount;
            totalFilesCountLabel.Text = "Total Files: " + totalFilesCount;
            elapsedTimeLabel.Text = "Elapsed Time: " + (DateTime.Now - startTime);
        }
    }
}
