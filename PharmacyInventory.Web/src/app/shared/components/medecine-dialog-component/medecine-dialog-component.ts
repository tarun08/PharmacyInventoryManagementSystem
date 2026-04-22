import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
    selector: 'app-medecine-dialog-component',
    imports: [ReactiveFormsModule],
    templateUrl: './medecine-dialog-component.html',
    styleUrl: './medecine-dialog-component.scss',
    standalone: true
})
export class MedecineDialogComponent {
    medicineForm: FormGroup;
    private dialogRef: MatDialogRef<MedecineDialogComponent>

    constructor(private fb: FormBuilder, dialogRef: MatDialogRef<MedecineDialogComponent>) {
        this.dialogRef = dialogRef;
        this.medicineForm = this.fb.group({
            fullName: ['', Validators.required],
            notes: [''],
            expiryDate: ['', Validators.required],
            quantity: [0, [Validators.required, Validators.min(1)]],
            price: [0, [Validators.required]],
            brand: ['', Validators.required]
        });
    }

    onSubmit() {
        if (this.medicineForm.valid) {
            console.log(this.medicineForm.value);
        }
        this.dialogRef.close(this.medicineForm.value);
    }

    onCancel(): void {
        this.dialogRef.close();
    }
}
