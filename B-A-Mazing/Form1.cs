using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace B_A_Mazing
{
    public partial class Form1 : Form
    {
        Bitmap InputImage, AMazeImage;

        public struct Position
        {
            private int x;
            private int y;

            public int X
            {
                get { return x; }
                set { x = value; }
            }
            public int Y
            {
                get { return y; }
                set { y = value; }
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonSaveImage_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.CreatePrompt = true;
                saveFileDialog.DefaultExt = "jpg";
                saveFileDialog.Filter = "File Images (*.jpg;*.jpeg;) | *.jpg;*.jpeg; |PNG Images | *.png |GIF Images | *.GIF";
                saveFileDialog.InitialDirectory = "c:\\gegevens\\prive\\tekenen\\";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        AMazeImage.Save(saveFileDialog.FileName);
                        buttonSaveImage.Enabled = false;
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("There was an error." +
                            "File could not be saved.");
                    }
                }
            }
        }

        private void buttonAMaze_Click(object sender, EventArgs e)
        {
            Position CurrentPosition = new Position();
            int NoOfExits, NoOfPasses, MaxPasses;
            bool DeadEndsFound;
            DateTime StartTimePrunning = new DateTime();
            DateTime EndTimePrunning = new DateTime();

            richTextBoxMessages.Text = richTextBoxMessages.Text + "\n------------------";

            MaxPasses = (int)numericUpDownPrunning.Value;
            if (MaxPasses > 0)
            {
                NoOfPasses = 0;
                StartTimePrunning = DateTime.Now;
                do
                {
                    DeadEndsFound = false;
                    for (CurrentPosition.Y = 1; CurrentPosition.Y <= AMazeImage.Height - 2; CurrentPosition.Y++)
                    {
                        for (CurrentPosition.X = 1; CurrentPosition.X <= AMazeImage.Width - 2; CurrentPosition.X++)
                        {
                            if (AMazeImage.GetPixel(CurrentPosition.X, CurrentPosition.Y).R == 255)
                            {
                                NoOfExits = 4;
                                if (AMazeImage.GetPixel(CurrentPosition.X, CurrentPosition.Y - 1).R == 0) { NoOfExits--; };
                                if (AMazeImage.GetPixel(CurrentPosition.X, CurrentPosition.Y + 1).R == 0) { NoOfExits--; };
                                if (AMazeImage.GetPixel(CurrentPosition.X - 1, CurrentPosition.Y).R == 0) { NoOfExits--; };
                                if (AMazeImage.GetPixel(CurrentPosition.X + 1, CurrentPosition.Y).R == 0) { NoOfExits--; };
                                if (NoOfExits <= 1)
                                {
                                    AMazeImage.SetPixel(CurrentPosition.X, CurrentPosition.Y, Color.FromArgb(0, 0, 128));
                                    DeadEndsFound = true;
                                }
                            }
                        }
                    }
                    NoOfPasses = NoOfPasses + 1;
                } while (DeadEndsFound && (NoOfPasses < MaxPasses));
                EndTimePrunning = DateTime.Now;
                richTextBoxMessages.Text = richTextBoxMessages.Text + "\nPrunning passes: " + NoOfPasses;
                if (NoOfPasses == MaxPasses) { richTextBoxMessages.Text = richTextBoxMessages.Text + " (limited)"; };
                richTextBoxMessages.Text = richTextBoxMessages.Text + "\nPrunning time: \t" + (EndTimePrunning - StartTimePrunning).TotalMilliseconds + " mSec";
            }
            else
            {
                richTextBoxMessages.Text = richTextBoxMessages.Text + "\nNo Prunning"; 
            }

            Position StartPositionMaze = new Position();
            Position[] Path = new Position[2];
            Position[] PathNew;
            List<Position[]> PathList = new List<Position[]>();

            //Locate the startposition in the top row
            CurrentPosition.Y = 0;
            for (CurrentPosition.X = 1; CurrentPosition.X <= AMazeImage.Width - 2; CurrentPosition.X++)
            {
                if (AMazeImage.GetPixel(CurrentPosition.X, CurrentPosition.Y).R == 255)
                {
                    StartPositionMaze = CurrentPosition;
                    Path[0] = StartPositionMaze;
                    AMazeImage.SetPixel(StartPositionMaze.X, StartPositionMaze.Y, Color.FromArgb(0, 0, 255));
                    CurrentPosition.X = StartPositionMaze.X;
                    CurrentPosition.Y = StartPositionMaze.Y + 1;
                    Path[1] = CurrentPosition;
                    PathList.Add(Path);
                    break;
                }
            }
            if (PathList.Count == 0) { richTextBoxMessages.Text = richTextBoxMessages.Text + "\nNo start position found."; };

            DateTime StartTimeSolving, EndTimeSolving;
            StartTimeSolving = DateTime.Now;

            while (PathList.Count() > 0)
            {
                CurrentPosition = PathList[0][PathList[0].Length - 1];

                //Mark current position as 'no exit' i.e. path not available since it's already part of a path
                AMazeImage.SetPixel(CurrentPosition.X, CurrentPosition.Y, Color.FromArgb(0, 0, 255));

                //Scan surrounding for available exits i.e. possible paths if found then add this as a 
                //new path to the list

                //Check down position first since it's the only possiblity for an exit that ends the maze
                if (AMazeImage.GetPixel(CurrentPosition.X, CurrentPosition.Y + 1).R == 255)
                {
                    PathNew = PathList[0];
                    Array.Resize(ref PathNew, PathNew.Length + 1);
                    PathNew[PathNew.Length - 1].X = CurrentPosition.X;
                    PathNew[PathNew.Length - 1].Y = CurrentPosition.Y + 1;
                    PathList.Add(PathNew);

                    //Check if the path ends at the exit (bottom row), if so quit checking
                    if (CurrentPosition.Y + 1 == AMazeImage.Height - 1) { break; };
                }

                //Check up position
                if (AMazeImage.GetPixel(CurrentPosition.X, CurrentPosition.Y - 1).R == 255)
                {
                    PathNew = PathList[0];
                    Array.Resize(ref PathNew, PathNew.Length + 1);
                    PathNew[PathNew.Length - 1].X = CurrentPosition.X;
                    PathNew[PathNew.Length - 1].Y = CurrentPosition.Y - 1;
                    PathList.Add(PathNew);
                }

                //Check left position
                if (AMazeImage.GetPixel(CurrentPosition.X - 1, CurrentPosition.Y).R == 255)
                {
                    PathNew = PathList[0];
                    Array.Resize(ref PathNew, PathNew.Length + 1);
                    PathNew[PathNew.Length - 1].X = CurrentPosition.X - 1;
                    PathNew[PathNew.Length - 1].Y = CurrentPosition.Y;
                    PathList.Add(PathNew);
                }
                
                //Check right position
                if (AMazeImage.GetPixel(CurrentPosition.X + 1, CurrentPosition.Y).R == 255)
                {
                    PathNew = PathList[0];
                    Array.Resize(ref PathNew, PathNew.Length + 1);
                    PathNew[PathNew.Length - 1].X = CurrentPosition.X + 1;
                    PathNew[PathNew.Length - 1].Y = CurrentPosition.Y;
                    PathList.Add(PathNew);
                }

                //This path has been processed so remove it from the list
                PathList.RemoveAt(0);
            }

            EndTimeSolving = DateTime.Now;
            richTextBoxMessages.Text = richTextBoxMessages.Text + "\nSolving time: \t" + (EndTimeSolving - StartTimeSolving).TotalMilliseconds + " mSec";
            richTextBoxMessages.Text = richTextBoxMessages.Text + "\nTotal time: \t" + ((EndTimePrunning - StartTimePrunning) + (EndTimeSolving - StartTimeSolving)).TotalMilliseconds + " mSec";

            //The last path in the list contains the shortest route, color this path green
            if (PathList.Count() > 0)
            {
                for (int PathCounter = 0; PathCounter <= PathList.Last().Count() - 1; PathCounter++)
                {
                    CurrentPosition = PathList.Last()[PathCounter];
                    AMazeImage.SetPixel(CurrentPosition.X, CurrentPosition.Y, Color.FromArgb(0, 255, 0));
                }
            }
            else
            {
                richTextBoxMessages.Text = richTextBoxMessages.Text + "\nNo end position found.";
            }

            //Update the screen and enable the option to save the solution
            pictureBoxMazeInput.Refresh();
            buttonSaveImage.Enabled = true;
            buttonAMaze.Enabled = false;
        }

        private void numericUpDownPrunning_ValueChanged(object sender, EventArgs e)
        {

        }

        private void buttonLoadImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\gegevens\\prive\\tekenen\\";
                openFileDialog.Filter = "PNG|*.png|JPEG|*.jpg|Bitmap|*.bmp";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        InputImage = new Bitmap(@openFileDialog.FileName, true);
                        AMazeImage = new Bitmap(InputImage.Width, InputImage.Height);

                        int CurrentX, CurrentY;

                        for (CurrentX = 0; CurrentX < InputImage.Width; CurrentX++)
                        {
                            for (CurrentY = 0; CurrentY < InputImage.Height; CurrentY++)
                            {
                                AMazeImage.SetPixel(CurrentX, CurrentY, InputImage.GetPixel(CurrentX, CurrentY));
                            }
                        }

                        pictureBoxMazeInput.Image = AMazeImage;
                        buttonSaveImage.Enabled = false;
                        buttonAMaze.Enabled = true;
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("There was an error." +
                            "Check the path to the image file.");
                    }
                }
            }




        }
    }
}
