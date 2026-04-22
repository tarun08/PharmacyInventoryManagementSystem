import { Component, OnInit } from '@angular/core';
import { Medicine } from '../../models/medicine.model';
import { CommonModule } from '@angular/common';
import { ExpiryColorPipe } from '../../../shared/pipes/expiry-color-pipe';
import { InverntoryColorPipePipe } from '../../../shared/pipes/inverntory-color-pipe-pipe';
import { FormsModule } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MedecineDialogComponent } from '../../../shared/components/medecine-dialog-component/medecine-dialog-component';

@Component({
    selector: 'app-inventory-component',
    templateUrl: './inventory-component.html',
    styleUrl: './inventory-component.scss',
    imports: [CommonModule, FormsModule, ExpiryColorPipe, InverntoryColorPipePipe, MedecineDialogComponent],
    standalone: true
})
export class InventoryComponent implements OnInit {

    medicines: Medicine[] = [];
    searchText: string = '';

    constructor(private dialog: MatDialog) {

    }

    ngOnInit(): void {
        this.getMedicines();
    }

    getMedicines() {
        const medicines = [
            {
                "id": 1,
                "fullName": "Paracetamol 500mg",
                "notes": "Used for fever and mild pain relief",
                "expiryDate": "2026-12-31",
                "quantity": 25,
                "price": 10.50,
                "brand": "Cipla"
            },
            {
                "id": 2,
                "fullName": "Ibuprofen 200mg",
                "notes": "Anti-inflammatory and pain reliever",
                "expiryDate": "2026-06-15",
                "quantity": 8,
                "price": 15.00,
                "brand": "Dr. Reddy's"
            },
            {
                "id": 3,
                "fullName": "Amoxicillin 250mg",
                "notes": "Antibiotic for bacterial infections",
                "expiryDate": "2025-11-10",
                "quantity": 5,
                "price": 45.75,
                "brand": "Sun Pharma"
            },
            {
                "id": 4,
                "fullName": "Cetirizine 10mg",
                "notes": "Antihistamine for allergy relief",
                "expiryDate": "2027-03-20",
                "quantity": 50,
                "price": 12.00,
                "brand": "Zydus"
            },
            {
                "id": 5,
                "fullName": "Azithromycin 500mg",
                "notes": "Broad-spectrum antibiotic",
                "expiryDate": "2025-09-05",
                "quantity": 12,
                "price": 89.99,
                "brand": "Alkem"
            },
            {
                "id": 6,
                "fullName": "Metformin 500mg",
                "notes": "Used for type 2 diabetes management",
                "expiryDate": "2027-01-01",
                "quantity": 100,
                "price": 5.25,
                "brand": "USV"
            }];

        this.medicines = medicines.map(med => ({
            ...med,
            expiryDate: new Date(med.expiryDate)
        }));
    }

    search() {
        this.medicines = this.medicines.filter(med => med.fullName.toLowerCase().includes(this.searchText.toLowerCase()));
    }

    clearSearch() {
        this.searchText = '';
        this.getMedicines();
    }

    openAddMedicineModal() {
        const dialogRef = this.dialog.open(MedecineDialogComponent, {
            width: '400px'
        });

        dialogRef.afterClosed().subscribe(result => {
            if (result) {
                console.log('Received from modal:', result);

                this.medicines.push(result);
            }
        });
    }
}
