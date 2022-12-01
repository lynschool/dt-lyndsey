using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinterLab4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /**
         * Lab #4 - Vacation Booking by Lyndsey Winter, completed on November 14th.
         * 
         * In this program, we prompt the user - or, a trip advisor - to supply information about the trip, and
         * we provide the user with information about how much the trip will cost.
         */

        private void Form1_Load(object sender, EventArgs e)
        {
            // Reset the window to the default state.
            ResetTrip();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // Reset the window to the default state.
            ResetTrip();
        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            bool isValidData;
            int numPeople;

            // Try to convert the user's input to a number
            isValidData = int.TryParse(txtPeople.Text, out numPeople);
            if (isValidData)
            {
                // Make sure we're only booking for 1-10 people
                if (numPeople < 1 || numPeople > 10)
                {
                    DisplayMsg("People must be between 1-10", "Input Error");
                    txtPeople.Focus();
                    txtPeople.SelectAll();
                } else
                {
                    // If the user is traveling with 1 or 3 people, tell them about the BOGO offer.
                    switch (numPeople)
                    {
                        case 1:
                        case 3:
                            DisplayMsg("Special when booking single or triple.\nBOGO Special - Call 555-1212 to receive another person free!", "Limited Time Offer");
                            break;
                    }

                    // By default, every location shares the same price per person which is $2,150.50, but Mexico has a
                    // price of $2,300.79 per person so we'll check for that
                    double pricePerPerson = 2150.50;
                    if (radMexico.Checked)
                    {
                        pricePerPerson = 2300.79;
                    }

                    // Calculate the price, and if the user is paying with cash then we apply a 10% discount.
                    double totalPrice = pricePerPerson * numPeople;
                    if (radCash.Checked)
                    {
                        totalPrice = totalPrice * 0.9;
                    }

                    // Determine the location the user is traveling to.
                    string bookedLocation = "UNKNOWN";
                    if (radMexico.Checked)
                    {
                        bookedLocation = "MEXICO";
                    } else if (radCuba.Checked)
                    {
                        bookedLocation = "CUBA";
                    } else if (radFlorida.Checked)
                    {
                        bookedLocation = "FLORIDA";
                    }

                    // If the user is getting the cash discount or the flight is included, we have to tell them about it.
                    string additionalInfo = "";
                    if (radCash.Checked)
                    {
                        additionalInfo += "Cash Discount Applied\n";
                    }
                    if (chkFlightIncluded.Checked)
                    {
                        additionalInfo += "Flight Included\n";
                    }

                    // Display the overview about the trip
                    lblBookInfo.Text = "Booked by Lyndsey Winter\n\n" +
                        "People: " + numPeople.ToString("D2") + "\n" +
                        "Location: " + bookedLocation.ToUpper() + "\n" +
                        additionalInfo +
                        "Price: " + totalPrice.ToString("C2");
                    lblPrice.Text = totalPrice.ToString("C2");

                    // Display the Trip Information group box, and disable interactions with the Booking group box.
                    grpTripInfo.Visible = true;
                    grpBook.Enabled = false;
                }
            } else
            {
                // The data the user entered isn't valid - tell them, and let them try again.
                DisplayMsg("People must be a whole number", "Input Error");
                txtPeople.Focus();
                txtPeople.SelectAll();
            }
        }

        private void radCuba_CheckedChanged(object sender, EventArgs e)
        {
            // Update the UI to show if there's flights included for this location
            SetFlight();
        }

        private void radFlorida_CheckedChanged(object sender, EventArgs e)
        {
            // Update the UI to show if there's flights included for this location
            SetFlight();
        }

        private void radMexico_CheckedChanged(object sender, EventArgs e)
        {
            // Update the UI to show if there's flights included for this location
            SetFlight();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            // When the user confirms their trip, tell them it was booked, they've got an email and how much they paid.
            DisplayMsg("Trip booked and paid\nPrice: " + lblPrice.Text, "Confirmation Email Sent");

            // Reset the UI to its default state.
            grpBook.Enabled = true;
            ResetTrip();
        }

        // ResetTrip function, resets the UI to its initial state.
        private void ResetTrip()
        {
            // Make Cuba be the default trip option, Credit Card the default payment
            // method, and remove the price since the trip hasn't been set yet.
            radCuba.Checked = true;
            radCreditCard.Checked = true;
            lblPrice.Text = "";

            // Hide the Trip Information until the trip has been booked.
            grpTripInfo.Visible = false;

            // Reset the number of people going on the trip, refocus on the box.
            txtPeople.Text = "";
            txtPeople.Focus();
        }

        // SetFlight function, determines whether there is a flight available for the location.
        private void SetFlight()
        {
            // Trips to Florida don't have a flight available - reflect this on the UI.
            if (radFlorida.Checked)
            {
                chkFlightIncluded.Checked = false;
            }
            else
            {
                chkFlightIncluded.Checked = true;
            }
        }

        // DisplayMsg function, takes two strings - message and title.
        private void DisplayMsg(string message, string title)
        {
            // Display a MessageBox to the user, using the specified message and title.
            MessageBox.Show(message, title);
        }
    }
}
