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
        public const string MazePath = "C:\\Gegevens\\Projecten\\Mazes\\Samples\\";
        public const string ImagesPath = "C:\\Gegevens\\Projecten\\Mazes\\Samples\\Images\\";

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
                saveFileDialog.InitialDirectory = MazePath;

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


        // ********************************************************
        // *********** Discover shortest possible route ***********
        // ********************************************************
        private void buttonAMaze_Click(object sender, EventArgs e)
        {
            Position CurrentPosition = new Position();
            Position[] Path = new Position[2];
            Position[] PathNew;
            List<Position[]> PathList = new List<Position[]>();

            richTextBoxMessages.Text = richTextBoxMessages.Text + "\n------------------";

            //Locate the startposition in the top row
            CurrentPosition.Y = 0;
            for (CurrentPosition.X = 1; CurrentPosition.X <= AMazeImage.Width - 2; CurrentPosition.X++)
            {
                if (AMazeImage.GetPixel(CurrentPosition.X, CurrentPosition.Y).R == 255)
                {
                    Path[0] = CurrentPosition;
                    AMazeImage.SetPixel(CurrentPosition.X, CurrentPosition.Y, Color.FromArgb(0, 0, 255));
                    CurrentPosition.Y = CurrentPosition.Y + 1;
                    Path[1] = CurrentPosition;
                    PathList.Add(Path);
                    break;
                }
            }
            if (PathList.Count == 0) 
            { 
                richTextBoxMessages.Text = richTextBoxMessages.Text + "\nNo start position found.";
                return;
            };

            DateTime StartTimeSolving, EndTimeSolving;
            StartTimeSolving = DateTime.Now;
            int FileCounter;            //Used to make animated GIF
            FileCounter = 1;            //Used to make animated GIF

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

                //                FileCounter = FileCounter + 1;
                //                AMazeImage.Save(ImagesPath + "Maze" + FileCounter + ".jpg"); //Used to make animated GIF

            }

            EndTimeSolving = DateTime.Now;
            if (PathList.Count() > 0)
            {
                richTextBoxMessages.Text = richTextBoxMessages.Text + "\nSolving time (I): \t" + (EndTimeSolving - StartTimeSolving).TotalMilliseconds + " mSec";
                richTextBoxMessages.Text = richTextBoxMessages.Text + "\nLength: \t\t" + PathList[PathList.Count - 1].Length;

                //The last path in the list contains the shortest route, color this path green
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
            buttonPrunning.Enabled = false;
            buttonAMaze.Enabled = false;
            buttonAMazeII.Enabled = false;
        }

        private void numericUpDownPrunning_ValueChanged(object sender, EventArgs e)
        {

        }

        // *********************************************************
        // *********** Discover a possible route quickly ***********
        // *********************************************************
        private void buttonAMazeII_Click(object sender, EventArgs e)
        {
            Position CurrentPosition = new Position();
            Position NewPosition = new Position();
            List<Position> Path = new List<Position>();
            List<int> AlternativePaths = new List<int>();
            int ExitFound;

            richTextBoxMessages.Text = richTextBoxMessages.Text + "\n------------------";

            //Locate the startposition in the top row
            CurrentPosition.Y = 0;
            ExitFound = 0;
            for (CurrentPosition.X = 1; CurrentPosition.X <= AMazeImage.Width - 2; CurrentPosition.X++)
            {
                if (AMazeImage.GetPixel(CurrentPosition.X, CurrentPosition.Y).R == 255)
                {
                    AMazeImage.SetPixel(CurrentPosition.X, CurrentPosition.Y, Color.FromArgb(0, 0, 255));
                    Path.Add(CurrentPosition);
                    CurrentPosition.Y = CurrentPosition.Y + 1;
                    Path.Add(CurrentPosition);
                    ExitFound = 1;
                    break;
                }
            }
            if (ExitFound == 0)
            {
                richTextBoxMessages.Text = richTextBoxMessages.Text + "\nNo start position found.";
                return;
            };

            DateTime StartTimeSolving, EndTimeSolving;
            StartTimeSolving = DateTime.Now;
            int FileCounter;            //Used to make animated GIF
            FileCounter = 1;            //Used to make animated GIF


            while (Path.Count > 0)
            {
                CurrentPosition = Path[Path.Count - 1];
                ExitFound = 0;

                //Mark current position as 'no exit' i.e. path not available since it's already part of a path
                AMazeImage.SetPixel(CurrentPosition.X, CurrentPosition.Y, Color.FromArgb(0, 0, 255));

                //Scan surrounding for available exits i.e. possible paths if found then add this as a 
                //new path to the list

                //Check down position first since it's the only possiblity for an exit that ends the maze
                if (AMazeImage.GetPixel(CurrentPosition.X, CurrentPosition.Y + 1).R == 255)
                {
                    NewPosition.X = CurrentPosition.X;
                    NewPosition.Y = CurrentPosition.Y + 1;
                    Path.Add(NewPosition);
                    ExitFound++;

                    //Check if the path exits at the maze end (bottom row), if so quit checking
                    if (NewPosition.Y == AMazeImage.Height - 1) { break; };
                }

                //Check left position
                if (AMazeImage.GetPixel(CurrentPosition.X - 1, CurrentPosition.Y).R == 255)
                {
                    if (ExitFound == 0)
                    {
                        NewPosition.X = CurrentPosition.X - 1;
                        NewPosition.Y = CurrentPosition.Y;
                        Path.Add(NewPosition);
                    }
                    ExitFound++;
                }

                //Check right position
                if (AMazeImage.GetPixel(CurrentPosition.X + 1, CurrentPosition.Y).R == 255)
                {
                    if (ExitFound == 0)
                    {
                        NewPosition.X = CurrentPosition.X + 1;
                        NewPosition.Y = CurrentPosition.Y;
                        Path.Add(NewPosition);
                    }
                    ExitFound++;
                }

                //Check up position
                if (AMazeImage.GetPixel(CurrentPosition.X, CurrentPosition.Y - 1).R == 255)
                {
                    if (ExitFound == 0)
                    {
                        NewPosition.X = CurrentPosition.X;
                        NewPosition.Y = CurrentPosition.Y - 1;
                        Path.Add(NewPosition);
                    }
                    ExitFound++;
                }

                if (ExitFound > 1) { AlternativePaths.Add(Path.Count - 2); } //Add current entry (current position) to list of alternative route possibilities
                if (ExitFound == 0)
                {
                    if (AlternativePaths.Count > 0) // Check if alternative route possibilities exist
                    {
                        Path.RemoveRange(AlternativePaths[AlternativePaths.Count - 1] + 1, Path.Count - AlternativePaths[AlternativePaths.Count - 1] - 1);
                        AlternativePaths.RemoveAt(AlternativePaths.Count - 1);
                    }
                    else
                    { Path.Clear(); }
                }


                //                FileCounter = FileCounter + 1;
                //                AMazeImage.Save(ImagesPath + "Maze" + FileCounter + ".jpg"); //Used to make animated GIF

            }

            EndTimeSolving = DateTime.Now;
            if (Path.Count() > 0)
            {
                richTextBoxMessages.Text = richTextBoxMessages.Text + "\nSolving time (III): \t" + (EndTimeSolving - StartTimeSolving).TotalMilliseconds + " mSec";
                richTextBoxMessages.Text = richTextBoxMessages.Text + "\nLength: \t\t" + Path.Count;

                //The last path in the list contains the shortest route, color this path green
                for (int PathCounter = 0; PathCounter <= Path.Count() - 1; PathCounter++)
                {
                    CurrentPosition = Path[PathCounter];
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
            buttonPrunning.Enabled = false;
            buttonAMaze.Enabled = false;
            buttonAMazeII.Enabled = false;
        }

        // ****************************************************
        // *********** Discover all possible routes ***********
        // ****************************************************
        private void buttonPrunning_Click(object sender, EventArgs e)
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

            //Update the screen and enable the option to save the solution
            pictureBoxMazeInput.Refresh();
            buttonSaveImage.Enabled = true;
            buttonPrunning.Enabled = false;
            buttonAMaze.Enabled = false;
            buttonAMazeII.Enabled = false;
        }

        private void buttonLoadImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = MazePath;
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
                        buttonPrunning.Enabled = true;
                        buttonAMaze.Enabled = true;
                        buttonAMazeII.Enabled = true;
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
