import { Component, OnInit, inject, signal } from '@angular/core';
import { Medicine } from '../../models/medicine.model';
import { CommonModule } from '@angular/common';
import { ExpiryColorPipe } from '../../../shared/pipes/expiry-color-pipe';
import { InverntoryColorPipePipe } from '../../../shared/pipes/inverntory-color-pipe-pipe';
import { FormsModule } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MedecineDialogComponent } from '../../../shared/components/medecine-dialog-component/medecine-dialog-component';
import { InventoryService } from '../../services/inventory-service';

@Component({
    selector: 'app-inventory-component',
    templateUrl: './inventory-component.html',
    styleUrl: './inventory-component.scss',
    imports: [CommonModule, FormsModule, ExpiryColorPipe, InverntoryColorPipePipe, MedecineDialogComponent],
    standalone: true
})
export class InventoryComponent implements OnInit {

    medicines = signal<Medicine[]>([]);
    searchText = signal<string>('');

    private inventoryService = inject(InventoryService);

    constructor(private dialog: MatDialog) {

    }

    ngOnInit(): void {
        this.getMedicines();
    }

    getMedicines() {
        this.inventoryService.getInventory().subscribe({
            next: (data) => {
                this.medicines.set(data.map(med => ({
                    ...med,
                    expiryDate: new Date(med.expiryDate)
                })));
            },
            error: (err) => console.error('Error fetching medicines', err)
        });
    }

    search() {
        this.inventoryService.getInventory(this.searchText()).subscribe({
            next: (data) => {
                this.medicines.set(data.map(med => ({
                    ...med,
                    expiryDate: new Date(med.expiryDate)
                })));
            },
            error: (err) => console.error('Error searching medicines', err)
        });
    }


    clearSearch() {
        this.searchText.set('');
        this.getMedicines();
    }

    openAddMedicineModal() {
        const dialogRef = this.dialog.open(MedecineDialogComponent, {
            width: '400px'
        });

        dialogRef.afterClosed().subscribe(result => {
            if (result) {
                console.log('Received from modal:', result);

                this.addMedecine(result);
            }
        });
    }

    addMedecine(med: Medicine) {
        this.inventoryService.addMedicine([med]).subscribe({
            next: (data) => {
                 this.getMedicines();
            },
            error: (err) => console.error('Error adding medicine', err)
        });
    }
}
