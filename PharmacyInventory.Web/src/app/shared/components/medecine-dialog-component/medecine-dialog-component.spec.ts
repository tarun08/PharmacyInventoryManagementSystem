import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MedecineDialogComponent } from './medecine-dialog-component';

describe('MedecineDialogComponent', () => {
  let component: MedecineDialogComponent;
  let fixture: ComponentFixture<MedecineDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MedecineDialogComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(MedecineDialogComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
