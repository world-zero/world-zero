import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditPraxisComponent } from './edit-praxis.component';

describe('EditPraxisComponent', () => {
  let component: EditPraxisComponent;
  let fixture: ComponentFixture<EditPraxisComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditPraxisComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditPraxisComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
