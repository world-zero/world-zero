import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import {FormControl, FormGroup} from '@angular/forms';
import { CKEditorModule } from 'ckeditor4-angular';

@Component({
  selector: 'app-edit-praxis',
  templateUrl: './edit-praxis.component.html',
  styleUrls: ['./edit-praxis.component.less'],
  providers: [ CKEditorModule ]
})

export class EditPraxisComponent implements OnInit {
  myForm: FormGroup;

  constructor() { }

  ngOnInit(){
    this.myForm = new FormGroup({ sampleForm: new FormControl(null)} );
  }
}
