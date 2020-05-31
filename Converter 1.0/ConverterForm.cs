using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Converter_1._0
{
    public partial class ConverterForm : Form
    {
        public ConverterForm()
        {
            InitializeComponent();
            pictureBoxLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxLogo.Image = Properties.Resources.Converter_WAV_AIFF_1_0;            
        }

        FileStream fileStreamInput = null;
        string inputAudioFile=null;
        string currentAudioFormat = null;
        short[] audioData = null;
        OpenFileDialog ofd = null;

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openAudioFile();
        }
       
        private void openAudioFile()
        {
            using (ofd = new OpenFileDialog() { Filter = "All files|*.*|WAV|*.wav|AIFF|*.aiff" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    inputAudioFile = ofd.FileName;
                    fileStreamInput = new FileStream(inputAudioFile, FileMode.Open, FileAccess.Read);

                    string[] safeAudioFileName = ofd.SafeFileName.Split(new char[] { '.' });

                    switch (safeAudioFileName[safeAudioFileName.Length - 1])
                    {
                        case "wav":
                            currentAudioFormat = "wav";
                            WaveFileReader waveFileReader = new WaveFileReader(fileStreamInput);
                            showWavHeader(waveFileReader, ofd);
                            convertToolStripMenuItem.Enabled= comboBoxFormats.Enabled = buttonConvert.Enabled = true;
                            comboBoxFormats.SelectedIndex = 0;
                            printAudioWave();
                            break;
                        case "aiff":
                            currentAudioFormat = "aiff";
                            AiffFileReader aiffFileReader = new AiffFileReader(fileStreamInput);
                            showAiffHeader(aiffFileReader, ofd);
                            convertToolStripMenuItem.Enabled=comboBoxFormats.Enabled = buttonConvert.Enabled = true;
                            comboBoxFormats.SelectedIndex = 0;
                            printAudioWave();
                            break;
                        default:
                            currentAudioFormat = null;
                            break;
                    }
                }
            }
        }

        private void printAudioWave()
        {
            BinaryReader binRead = null;
            uint dataSize = 0;
            int numSamples = 0;
            Point[] points = new Point[pictureBox.Width];
            int coefForWidth = 0;
            int coeffForHeight = 0;
            Pen penCountur = new Pen(Color.Black, 2);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            StringFormat drawFormat = new StringFormat();
            Graphics graphicMain = pictureBox.CreateGraphics();
            switch (currentAudioFormat)
            {
                case "wav":
                    Cursor.Current = Cursors.WaitCursor;
                    fileStreamInput.Read(new byte[4], 0, 4);
                    binRead = new BinaryReader(fileStreamInput);
                    dataSize = (uint)(fileStreamInput.Length / 4);
                    audioData = new Int16[dataSize];
                    fileStreamInput.Seek(40, System.IO.SeekOrigin.Begin);
                    numSamples = (int)(dataSize / 1);
                    for (int i = 0; i < numSamples; i++)
                    {
                        audioData[i] = binRead.ReadInt16();
                    }
                    coefForWidth = (int)(audioData.Length / (pictureBox.Width));
                    coeffForHeight = (int)(65536 / (pictureBox.Height - 40));
                    for (int i = 0; i < pictureBox.Width; i++)
                    {
                        points[i] = new Point(i, (int)((audioData[i * coefForWidth] / coeffForHeight) + (pictureBox.Height / 2)));
                    }
                    graphicMain.Clear(Color.White);                    
                    graphicMain.DrawLines(penCountur, points);
                    Cursor.Current = Cursors.Default;
                    break;
                case "aiff":
                    Cursor.Current = Cursors.WaitCursor;
                    string tempFileName = Path.Combine(Environment.CurrentDirectory, "temp.wav");
                    using (AiffFileReader reader = new AiffFileReader(inputAudioFile))
                    {
                        using (WaveFileWriter writer = new WaveFileWriter(tempFileName, reader.WaveFormat))
                        {
                            byte[] buffer = new byte[4096];
                            int bytesRead = 0;
                            do
                            {
                                bytesRead = reader.Read(buffer, 0, buffer.Length);
                                writer.Write(buffer, 0, bytesRead);
                            } while (bytesRead > 0);
                        }
                    }
                    FileStream tempFileStreamInput = new FileStream(tempFileName, FileMode.Open, FileAccess.Read);
                    tempFileStreamInput.Read(new byte[4], 0, 4);
                    binRead = new BinaryReader(tempFileStreamInput);
                    dataSize = (uint)(tempFileStreamInput.Length / 4);
                    audioData = new Int16[dataSize];
                    tempFileStreamInput.Seek(40, System.IO.SeekOrigin.Begin);
                    numSamples = (int)(dataSize / 1);
                    for (int i = 0; i < numSamples; i++)
                    {
                        audioData[i] = binRead.ReadInt16();
                    }
                    tempFileStreamInput.Close();
                    File.Delete(tempFileName);
                    coefForWidth = (int)(audioData.Length / (pictureBox.Width));
                    coeffForHeight = (int)(65536 / (pictureBox.Height - 40));
                    for (int i = 0; i < pictureBox.Width; i++)
                    {
                        points[i] = new Point(i, (int)((audioData[i * coefForWidth] / coeffForHeight) + (pictureBox.Height / 2)));
                    }
                    graphicMain.Clear(Color.White);
                    graphicMain.DrawLines(penCountur, points);
                    Cursor.Current = Cursors.Default;
                    break;
                default:                    
                    break;
            }            
        }

        public void convertAiffToWav(string aiffFile, string wavFile)
        {
            using (AiffFileReader reader = new AiffFileReader(@aiffFile))
            {
                using (WaveFileWriter writer = new WaveFileWriter(@wavFile, reader.WaveFormat))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead = 0;
                    do
                    {
                        bytesRead = reader.Read(buffer, 0, buffer.Length);
                        writer.Write(buffer, 0, bytesRead);
                    } while (bytesRead > 0);
                }
            }
        }

        public void convertWavToAiff(string wavFile, string aiffFile)
        {
            using (WaveFileReader reader = new WaveFileReader(@wavFile))
            {
                using (AiffFileWriter writer = new AiffFileWriter(@aiffFile, reader.WaveFormat))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead = 0;
                    do
                    {
                        bytesRead = reader.Read(buffer, 0, buffer.Length);
                        writer.Write(buffer, 0, bytesRead);
                    } while (bytesRead > 0);
                }
            }
        }

        private void showWavHeader(WaveFileReader waveFileReader, OpenFileDialog ofd)
        {
            listView.Clear();

            Dictionary<string, string> locationData = new Dictionary<string, string>();
            Dictionary<string, string> generalData = new Dictionary<string, string>();

            ColumnHeader columnHeaderName = new ColumnHeader();
            columnHeaderName.Text = "Name";
            columnHeaderName.Width = 150;
            ColumnHeader columnHeaderValue = new ColumnHeader();
            columnHeaderValue.Text = "Value";
            columnHeaderValue.Width = 350;

            listView.Columns.AddRange(new ColumnHeader[] { columnHeaderName, columnHeaderValue });
            ListViewGroup listViewGroupLocation = new ListViewGroup("Location");
            ListViewGroup listViewGroupGeneral = new ListViewGroup("General");
            listView.Groups.Add(listViewGroupLocation);
            listView.Groups.Add(listViewGroupGeneral);

            //locationData
            locationData.Add("File name", ofd.SafeFileName);
            locationData.Add("Folder name", ofd.FileName);
            locationData.Add("File size", waveFileReader.Length + " (bytes)");
            locationData.Add("Last modified", File.GetLastWriteTime(ofd.FileName).ToString());
            locationData.Add("Created", File.GetCreationTime(ofd.FileName).ToString());
            foreach (var item in locationData)
            {
                ListViewItem listViewItem = new ListViewItem(item.Key, listViewGroupLocation);
                listViewItem.SubItems.Add(item.Value);
                listView.Items.Add(listViewItem);
            }
            //generalData
            generalData.Add("Duration", waveFileReader.TotalTime.ToString());
            generalData.Add("Sample Rate", waveFileReader.WaveFormat.SampleRate.ToString() + " Hz");
            generalData.Add("Channels", waveFileReader.WaveFormat.Channels.ToString());
            generalData.Add("Bits per sample", waveFileReader.WaveFormat.BitsPerSample.ToString());
            generalData.Add("Average bytes per second", waveFileReader.WaveFormat.AverageBytesPerSecond.ToString());
            generalData.Add("Block Align", waveFileReader.WaveFormat.BlockAlign.ToString());
            foreach (var item in generalData)
            {
                ListViewItem listViewItem = new ListViewItem(item.Key, listViewGroupGeneral);
                listViewItem.SubItems.Add(item.Value);
                listView.Items.Add(listViewItem);
            }
        }

        private void showAiffHeader(AiffFileReader aiffFileReader, OpenFileDialog ofd)
        {
            listView.Clear();

            Dictionary<string, string> locationData = new Dictionary<string, string>();
            Dictionary<string, string> generalData = new Dictionary<string, string>();

            ColumnHeader columnHeaderName = new ColumnHeader();
            columnHeaderName.Text = "Name";
            columnHeaderName.Width = 100;
            ColumnHeader columnHeaderValue = new ColumnHeader();
            columnHeaderValue.Text = "Value";
            columnHeaderValue.Width = 300;

            listView.Columns.AddRange(new ColumnHeader[] { columnHeaderName, columnHeaderValue });
            ListViewGroup listViewGroupLocation = new ListViewGroup("Location");
            ListViewGroup listViewGroupGeneral = new ListViewGroup("General");
            listView.Groups.Add(listViewGroupLocation);
            listView.Groups.Add(listViewGroupGeneral);

            //locationData
            locationData.Add("File name", ofd.SafeFileName);
            locationData.Add("Folder name", ofd.FileName);
            locationData.Add("File size", aiffFileReader.Length + " (bytes)");
            locationData.Add("Last modified", File.GetLastWriteTime(ofd.FileName).ToString());
            locationData.Add("Created", File.GetCreationTime(ofd.FileName).ToString());
            foreach (var item in locationData)
            {
                ListViewItem listViewItem = new ListViewItem(item.Key, listViewGroupLocation);
                listViewItem.SubItems.Add(item.Value);
                listView.Items.Add(listViewItem);
            }
            //generalData
            generalData.Add("Duration", aiffFileReader.TotalTime.ToString());
            generalData.Add("Sample Rate", aiffFileReader.WaveFormat.SampleRate.ToString() + " Hz");
            generalData.Add("Channels", aiffFileReader.WaveFormat.Channels.ToString());
            generalData.Add("Bits per sample", aiffFileReader.WaveFormat.BitsPerSample.ToString());
            generalData.Add("Average bytes per second", aiffFileReader.WaveFormat.AverageBytesPerSecond.ToString());
            generalData.Add("Block Align", aiffFileReader.WaveFormat.BlockAlign.ToString());
            foreach (var item in generalData)
            {
                ListViewItem listViewItem = new ListViewItem(item.Key, listViewGroupGeneral);
                listViewItem.SubItems.Add(item.Value);
                listView.Items.Add(listViewItem);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            convert();
        }

        private void buttonConvert_Click(object sender, EventArgs e)
        {
            convert();
        }

        private void convert()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "All files|*.*|Wav files|*.wav|AIFF files|*.aiff";
            bool correctFormatSelected = false;
            switch (comboBoxFormats.SelectedIndex)
            {
                case 0:
                    if (currentAudioFormat=="aiff")
                    {
                        correctFormatSelected = false;
                        showConvertationError();
                    }
                    else
                    {
                        correctFormatSelected = true;
                        sfd.DefaultExt = ".aiff";
                    }
                    break;
                case 1:
                    if (currentAudioFormat == "wav")
                    {
                        correctFormatSelected = false;
                        showConvertationError();
                    }
                    else
                    {
                        correctFormatSelected = true;
                        sfd.DefaultExt = ".aiff";
                    }
                    sfd.DefaultExt = ".wav";
                    break;
                default:
                    break;
            }
            if (correctFormatSelected)
            {
                if (sfd.ShowDialog() == DialogResult.OK && sfd.FileName.Length > 0)
                {

                    fileStreamInput.Position = 0;
                    Cursor.Current = Cursors.WaitCursor;

                    switch (comboBoxFormats.SelectedIndex)
                    {
                        case 0:
                            convertWavToAiff(inputAudioFile, sfd.FileName);
                            break;
                        case 1:
                            convertAiffToWav(inputAudioFile, sfd.FileName);
                            break;
                        default:
                            break;
                    }
                    Cursor.Current = Cursors.Default;
                    Form formSucces = new Form();
                    formSucces.StartPosition = FormStartPosition.CenterParent;
                    formSucces.Size = new System.Drawing.Size(250, 100); ;
                    formSucces.MinimumSize = formSucces.MaximumSize = formSucces.Size;
                    Label labelSucces = new Label();
                    labelSucces.Location = new Point(40, 25);
                    labelSucces.Size = new System.Drawing.Size(200, 25); ;
                    formSucces.Controls.Add(labelSucces);
                    labelSucces.Text = "Process is succesfully finised!";
                    formSucces.ShowDialog();
                }
            }
            
        }

        private void showConvertationError()
        {
            Form formError = new Form();
            formError.Size = new System.Drawing.Size(300, 100); ;
            formError.StartPosition = FormStartPosition.CenterParent;
            formError.MinimumSize = formError.MaximumSize = formError.Size;
            Label labelSucces = new Label();
            labelSucces.Location = new Point(40, 25);
            labelSucces.Size = new System.Drawing.Size(200, 25); ;
            formError.Controls.Add(labelSucces);
            labelSucces.Text = "Can't convert into the same audio format!";
            formError.ShowDialog();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clearAllFields();
        }

        private void clearAllFields()
        {
            listView.Clear();
            Graphics graphicMain = pictureBox.CreateGraphics();
            graphicMain.Clear(Color.White);
            if (fileStreamInput!=null)
            {
                fileStreamInput.Close();
            }
            convertToolStripMenuItem.Enabled =buttonConvert.Enabled = comboBoxFormats.Enabled= false;
            GC.Collect();
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InfoForm infoForm = new InfoForm();
            infoForm.StartPosition = FormStartPosition.CenterScreen;
            infoForm.Show();
        }
    }
}
