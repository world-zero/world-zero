import { Component, ViewChild, OnInit } from '@angular/core';
import { MDBBootstrapModule } from 'angular-bootstrap-md';

@Component({
  selector: 'app-player',
  templateUrl: './player.component.html',
  styleUrls: ['./player.component.css'],
  providers: [ MDBBootstrapModule ]
})
export class PlayerComponent implements OnInit {
  constructor() { }

  ngOnInit(): void {
  }

}
