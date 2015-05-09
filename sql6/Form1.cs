using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace sql6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {



        }

        private void addToLog(string textToAdd)
        {

            txtLog.Text = txtLog.Text + " " + textToAdd;
        }

        private void addLineToLog(string textToAdd)
        {

            txtLog.Text = txtLog.Text + Environment.NewLine+ textToAdd + Environment.NewLine;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //adds a new customer with a bill (1 to many(bills) relationship)


            var newCustomer = new Customer();
            newCustomer.Name = "Jason";

            var newBill = new Bill();
            newBill.Amount = 300;
            // adds a list of Bill(s) to the customer in a 1(customer) to many(bills) relationship 
            newCustomer.Bills = new List<Bill>() { newBill };
            using (var db = new Model1())
            {
                addLineToLog("New Customer: " + newCustomer.Name + " added.");
                db.Customers.Add(newCustomer);
                db.SaveChanges();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {



            //Takes the textfield's text for the index of customer whom we will show their current bill(s) and then deletes the same customer and all bill related to the customer ( cascade delete )


            int tempID;


            tempID = Convert.ToInt32(textBox1.Text);
            using (var db = new Model1())
            {
                var person = db.Customers.Where(s => s.ID == tempID).Include(s => s.Bills).FirstOrDefault();
                addLineToLog("Showing all bill(s) amounts for " + person.Name + ":");
                foreach (var tempBill in person.Bills)
                {
                    //  MessageBox.Show(tempBill.Amount.ToString());
                    addLineToLog(tempBill.Amount.ToString());
                }

                //deletes the person and all associated bills
                db.Customers.Remove(person);
                db.SaveChanges();

            }


        }

        private void button3_Click(object sender, EventArgs e)
        {

            using (var db = new Model2())
            {

                // creates/adds video and playlist items in db with many to many association.

                var newVideo = new Video();
                var newPlaylist = new playlist();
                newVideo.Name = "New Video";
                newPlaylist.Name = "New Playlist";

                //uses many to many relationships for both video and playlist
                newVideo.Playlists = new List<playlist>() { newPlaylist };
                newPlaylist.Videos = new List<Video>() { newVideo };

                //updates log
                addLineToLog("New Video: " + newVideo.Name);
                addLineToLog("New Playlist: " + newPlaylist.Name);
                addLineToLog("Many2Many association created for " + newVideo.Name + " and " + newPlaylist.Name);
                
                //adds both the video and playlist to the db as well as their many to many association (table: videoplaylists) 
                db.Videos.Add(newVideo);
                db.playlists.Add(newPlaylist);
                db.SaveChanges();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            // many to many relationship. many different videos can be on many different playlists
            int tempID;

            //grabs the user input for the Video's ID to use in this example. *caution* currently Does not check for bad input
            tempID = Convert.ToInt32(textBox2.Text);

            //uses the model2 db (manytomany.db) 
            using (var db = new Model2())
            {
                //finds the video the ID of the users input tempID
                var tempVideo = db.Videos.Where(v => v.ID == tempID).Include(v => v.Playlists).FirstOrDefault();

                //lists all of the names of returned video's playlists 
                addLineToLog("Video: " + tempVideo.Name + " can be found on the following playlists:");
                foreach (var playlist in tempVideo.Playlists)
                {
                    //MessageBox.Show(playlist.Name);
                    addLineToLog(playlist.Name);
                }


                //removes any playlist's association with the returned video (table: videoplaylists)
                tempVideo.Playlists.Clear();
                db.SaveChanges();
            }
        }


    }
}
