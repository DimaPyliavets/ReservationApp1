// wwwroot/js/reservation-details.js

function printPDF(reservation) {
    var pdf = new jsPDF();

    pdf.text("Reservation Details", 20, 10);
    pdf.text("Reservation ID: " + reservation.ReservationId, 20, 20);
    pdf.text("Restaurant: " + reservation.Restaurant.RestaurantName, 20, 30);
    pdf.text("Reserved Zone: " + reservation.Zone.ZoneName, 20, 40);
    pdf.text("Reservation Date: " + reservation.ReservationDate.toDateString(), 20, 50);
    pdf.text("User Name: " + reservation.User.UserName, 20, 60);
    pdf.text("User Email: " + reservation.User.UserEmail, 20, 70);
    pdf.text("User Phone: " + reservation.User.UserPhone, 20, 80);

    pdf.save("reservation_details.pdf");
}
