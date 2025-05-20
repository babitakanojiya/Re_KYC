function openCity(cityName, Id2,flag) {

    debugger;
    //var i, tabcontent, tablinks;
    //tabcontent = document.getElementsByClassName("tabcontent");
    //for (i = 0; i < tabcontent.length; i++) {
    //    tabcontent[i].style.display = "none";
    //}
    //tablinks = document.getElementsByClassName("tablinks");
    //for (i = 0; i < tablinks.length; i++) {
    //    tablinks[i].className = tablinks[i].className.replace(" active", "");
    //}
    document.getElementById(cityName).style.display = "block";
    document.getElementById(Id2).style.display = "none";

    document.getElementById('1').style.backgroundColor = "#f1f1f1";
    document.getElementById('2').style.backgroundColor = "#f1f1f1";
    document.getElementById(flag).style.backgroundColor = "#ccc";



    //document.getElementById("tabid").value = cityName;
    //  evt.currentTarget.className += " active";
}

