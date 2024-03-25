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
        private int searchingStatusCode = 0;

        public MainForm()
        {
            InitializeComponent();
            Helper.EnableDoubleBuffering(filesTreeView);
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            if ((searchingStatusCode == 0) && (startDirectoryTextBox.Text != String.Empty))
            {
                startDirectory = startDirectoryTextBox.Text;
                fileNameRegex = new Regex(fileNameRegexTextBox.Text);
                searchingStatusCode = 1;

                // Clear previous search results
                ClearFormView();

                // Switch buttons availible
                searchButton.Enabled = false;    //Search - x
                pauseButton.Enabled = true;      //Pause  - v
                resumeButton.Enabled = false;    //Resume - x
                abortButton.Enabled = false;     //Abort  - x

                // Start search and timer in a separated threads
                Thread searchThread = new Thread(SearchFiles);
                searchThread.IsBackground = true;
                searchThread.Start();

                Thread timerThread = new Thread(TimerHandler);
                timerThread.IsBackground = true;
                timerThread.Start();
            }
            else
            {
                searchingStatusCode = 0;
            }
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            searchButton.Enabled = false;    //Search - x
            pauseButton.Enabled = false;     //Pause  - x
            resumeButton.Enabled = true;     //Resume - v
            abortButton.Enabled = true;      //Abort  - v

            searchingStatusCode = 2;
        }

        private void abortButton_Click(object sender, EventArgs e)
        {
            searchButton.Enabled = true;     //Search - v
            pauseButton.Enabled = false;     //Pause  - x
            resumeButton.Enabled = false;    //Resume - x
            abortButton.Enabled = false;     //Abort  - x

            searchingStatusCode = 0;
        }

        private void resumeButtin_Click(object sender, EventArgs e)
        {
            searchButton.Enabled = false;    //Search - x
            pauseButton.Enabled = true;      //Pause  - v
            resumeButton.Enabled = false;    //Resume - x
            abortButton.Enabled = false;     //Abort  - x

            searchingStatusCode = 1;
        }

        private void SearchFiles()
        {
            int foundFilesCount = 0;
            int totalFilesCount = 0;

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

                while ((searchingStatusCode != 0) && directories.Count > 0)
                {
                    while (searchingStatusCode == 2)
                    {
                        Thread.Sleep(100);
                    }

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

                        UpdateSearchStatus(currentDir.FullName, foundFilesCount, totalFilesCount);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        //��������� ������ 5. �������� � �������
                        continue;
                    }
                    catch (ObjectDisposedException)
                    {
                        //��� ������� �������� ��������� �� ����� ���������� ������, ��������� �� �������� ���������� Dequeue
                        continue;
                    }
                    catch (Exception ex)
                    {
                        searchingStatusCode = 0;

                        Invoke(new Action(() =>
                        {
                            ClearFormView();
                        }));

                        MessageBox.Show("��������� �������������� ������: " + ex.Message);

                        Invoke(new Action(() =>
                        {
                            elapsedTimeLabel.Text = "������� ������: ";
                        }));

                        break;
                    }
                }

                UpdateSearchStatus(String.Empty, foundFilesCount, totalFilesCount);
                MessageBox.Show("����� ��������!");
            }
            finally
            {
                searchingStatusCode = 0;
            }
        }

        private void TimerHandler()
        {
            DateTime startTime = DateTime.Now;

            while (searchingStatusCode != 0)
            {
                while (searchingStatusCode == 2)
                {
                    startTime = startTime.AddMilliseconds(100);
                    Thread.Sleep(100);
                }

                Thread.Sleep(25);
                UpdateElapsedTime(startTime);
            }

            UpdateElapsedTime(startTime);
        }

        private void UpdateSearchStatus(string currentDirectory, int foundFilesCount, int totalFilesCount)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => UpdateSearchStatus(currentDirectory, foundFilesCount, totalFilesCount)));
                return;
            }

            searchStatusLabel.Text = "������� ����������: " + currentDirectory;
            foundFilesCountLabel.Text = "������� ������: " + foundFilesCount;
            totalFilesCountLabel.Text = "����� ������: " + totalFilesCount;
        }

        private void UpdateElapsedTime(DateTime startTime)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => UpdateElapsedTime(startTime)));
                return;
            }

            elapsedTimeLabel.Text = "������� ������: " + (DateTime.Now - startTime);
        }

        private void ClearFormView()
        {
            filesTreeView.Nodes.Clear();
            searchStatusLabel.Text = "�����...";
            foundFilesCountLabel.Text = "";
            totalFilesCountLabel.Text = "";
            elapsedTimeLabel.Text = "";

            searchButton.Enabled = true;     //Search - v
            pauseButton.Enabled = false;     //Pause  - x
            resumeButton.Enabled = false;    //Resume - x
            abortButton.Enabled = false;     //Abort  - x
        }
    }
}
