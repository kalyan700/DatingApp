/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from './node_modules/@angular/core/testing';
import { By } from './node_modules/@angular/platform-browser';
import { DebugElement } from './node_modules/@angular/core';

import { ValueComponent } from './value.component';

describe('ValueComponent', () => {
  let component: ValueComponent;
  let fixture: ComponentFixture<ValueComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ValueComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ValueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
