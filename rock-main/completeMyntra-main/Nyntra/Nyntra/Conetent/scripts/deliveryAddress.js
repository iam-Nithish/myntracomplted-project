//// import navbar from "./navbar.js";
//// document.getElementById("navbar").innerHTML = navbar();
//let home = document.getElementById("home");
//let work = document.getElementById("work");
//// home.style.color = "#ff3f6c";
//// home.style.border = "1px solid #ff3f6c";


//home.addEventListener("click",saveHome)
//function saveHome() {

//event.preventDefault()

//  home.style.color = "hotpink";
//  home.style.border = "1px solid #ff3f6c";
//  work.style.color = "black";
//  work.style.border = "1px solid lightgray";
//}
//work.addEventListener("click",saveWork)
//function saveWork() {
//  event.preventDefault()
//  work.style.color = "hotpink";
//  work.style.border = "1px solid #ff3f6c";
//  home.style.color = "black";
//  home.style.border = "1px solid lightgray";
//}

//document.getElementById("addAddress").addEventListener("click", newAddress);
//// let totalPriceDelivery = JSON.parse(localStorage.getItem("totalprice"));
//let addressArr = [];
//function newAddress() {
//  event.preventDefault();



//  let name = document.getElementById("name").value;
//  let mobile = document.getElementById("Mobile").value;
//  let pincode = document.getElementById("pinCode").value;
//  let address = document.getElementById("address").value;
//  let town = document.getElementById("town").value;
//  let city = document.getElementById("city").value;
//  let state = document.getElementById("state").value;

//  let myAddress = new makeAddress(
//    name,
//    mobile,
//    pincode,
//    address,
//    town,
//    city,
//    state
//  );
//  if(name=="" && mobile=="" && pincode=="" && address=="" && town=="" && city=="" && state==""){
//    alert("All fields are required!")
//    return false;
//  }

//if(name==""){
//  alert("Please fill the name")
//  return false;
//}

//if(mobile==""){
//  alert("Mobile number is mandatory!")
//  return false;
//}
//if(pincode==""){
//  alert("Pincode is required!")
//  return false;
//}
//if(address==""){
//  alert("Address is required!")
//  return false;
//}
//if(town==""){
//  alert("Town/ Village is required! ")
//  return false;
//}
//if(city==""){
//  alert("City is required!")
//  return false;
//}
//if(state==""){
//  alert("State is required!")
//  return false;
//}

//else{




//  addressArr.push(myAddress);
//  localStorage.setItem("address", JSON.stringify(addressArr));
//    window.location.href = "https://localhost:44350/Home/selectadress";
//}

//function makeAddress(n, m, p, a, t, c, s) {
//  this.name = n;
//  this.mobile = m;
//  this.pincode = p;
//  this.address = a;
//  this.town = t;
//  this.city = c;
//  this.state = s;
//}



//return true;
//}
//let priceObj = JSON.parse(localStorage.getItem("totalPriceInfo"));
//let totalActual = priceObj.totalPrice;
//let total = priceObj.totalActualPrice;
//let discount = priceObj.discount;
//console.log(priceObj);

//document.getElementById("totalActual").innerText = `₹ ${total}`;
//document.getElementById("discount").innerText = `₹ ${discount}`;
//document.getElementById("totalP").innerText = `₹ ${totalActual}`;

// Assuming you're importing navbar somewhere
// import navbar from "./navbar.js";
// document.getElementById("navbar").innerHTML = navbar();

//let home = document.getElementById("home");
//let work = document.getElementById("work");

//home.addEventListener("click", saveHome);
//work.addEventListener("click", saveWork);
//document.getElementById("addAddress").addEventListener("click", newAddress);

//function saveHome(event) {
//    event.preventDefault();
//    home.style.color = "hotpink";
//    home.style.border = "1px solid #ff3f6c";
//    work.style.color = "black";
//    work.style.border = "1px solid lightgray";
//}

//function saveWork(event) {
//    event.preventDefault();
//    work.style.color = "hotpink";
//    work.style.border = "1px solid #ff3f6c";
//    home.style.color = "black";
//    home.style.border = "1px solid lightgray";
//}

//function newAddress(event) {
//    event.preventDefault();

//    // Fetching values from the form
//    let name = document.getElementById("name").value;
//    let mobile = document.getElementById("Mobile").value;
//    let pincode = document.getElementById("pinCode").value;
//    let address = document.getElementById("address").value;
//    let town = document.getElementById("town").value;
//    let city = document.getElementById("city").value;
//    let state = document.getElementById("state").value;

//    // Validating the input fields
//    if (!name || !mobile || !pincode || !address || !town || !city || !state) {
//        alert("All fields are required!");
//        return false;
//    }

//    // Creating the address object
//    let myAddress = new makeAddress(name, mobile, pincode, address, town, city, state);

//    // Send data to server
//    fetch("/Address/Create", { // Adjust the URL as necessary
//        method: "POST",
//        headers: {
//            "Content-Type": "application/json"
//        },
//        body: JSON.stringify(myAddress) // Convert the object to JSON
//    })
//    .then(response => {
//        if (response.ok) {
//            // If the request is successful, redirect
//            window.location.href = "https://localhost:44350/Home/selectadress";
//        } else {
//            alert("Failed to add address. Please try again.");
//        }
//    })
//    .catch(error => {
//        console.error("Error:", error);
//        alert("An error occurred while adding the address.");
//    });
//}

//function makeAddress(n, m, p, a, t, c, s) {
//    this.name = n;
//    this.mobile = m;
//    this.pincode = p;
//    this.address = a;
//    this.town = t;
//    this.city = c;
//    this.state = s;
//}

//// Fetching and displaying total price details
let priceObj = JSON.parse(localStorage.getItem("totalPriceInfo"));
if (priceObj) {
    let totalActual = priceObj.totalPrice;
    let total = priceObj.totalActualPrice;
    let discount = priceObj.discount;

    document.getElementById("totalActual").innerText = `₹ ${total}`;
    document.getElementById("discount").innerText = `₹ ${discount}`;
    document.getElementById("totalP").innerText = `₹ ${totalActual}`;
}
// Address.js
document.addEventListener("DOMContentLoaded", function () {
    // Example of handling form submission, etc.
    const addAddressButton = document.getElementById("addAddress");
    const totalP = document.getElementById("totalP");

    addAddressButton.addEventListener("click", function (e) {
        // You can perform any validations or calculations here
        alert("Address submitted!");
    });

    // Function to update total price, if needed
    //function updateTotalPrice() {
    //    // Logic to calculate total price goes here
    //    totalP.innerText = "₹200"; // Example total price
    //}

    //updateTotalPrice();
});
