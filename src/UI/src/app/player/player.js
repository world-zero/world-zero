/*
/*
function addRow() {

  var myName = document.getElementById("Player Name");
  var points = document.getElementById("Points");
  var level = document.getElementById("Level");
  var group = document.getElementById("Affiliation");


  var rowCount = table.rows.length;
  var row = table.insertRow(rowCount);

  row.insertCell(0).innerHTML= '<input type="button" value = "Delete" onClick="Javacsript:deleteRow(this)">';
  row.insertCell(1).innerHTML= myName.value;
  row.insertCell(2).innerHTML= points.value;
  row.insertCell(3).innerHTML= level.value;
  row.insertCell(4).innerHTML= group.value;



}
*/

/*
function deleteRow(obj) {

  var index = obj.parentNode.parentNode.rowIndex;
  var table = document.getElementById("myTableData");
  table.deleteRow(index);

}

function addTable() {

  var myTableDiv = document.getElementById("myDynamicTable");

  var table = document.createElement('TABLE');
  table.border='1';

  var tableBody = document.createElement('TBODY');
  table.appendChild(tableBody);

  for (var i=0; i<3; i++){
    var tr = document.createElement('TR');
    tableBody.appendChild(tr);

    for (var j=0; j<4; j++){
      var td = document.createElement('TD');
      td.width='75';
      td.appendChild(document.createTextNode("Cell " + i + "," + j));
      tr.appendChild(td);
    }
  }
  myTableDiv.appendChild(table);

}

function load() {

  console.log("Page load finished");

}

function toggleMenu() {
  var menu = document.getElementById("taskForm");
  var buttonText = document.getElementById("addTask")
  if(menu.style.display === "none"){
    menu.style.display = "block";
    buttonText.innerHTML = "Nevermind!"
  } else {
    menu.style.display = "none";
    buttonText.innerHTML = "Add a task!"
  }

}
*/
var coll = document.getElementsByClassName("collapsible");
var i;

for (i = 0; i < coll.length; i++) {
  coll[i].addEventListener("click", function() {
    this.classList.toggle("active");
    var content = this.nextElementSibling;
    if (content.style.maxHeight){
      content.style.maxHeight = null;
    } else {
      content.style.maxHeight = content.scrollHeight + "px";
    }
  });
}

