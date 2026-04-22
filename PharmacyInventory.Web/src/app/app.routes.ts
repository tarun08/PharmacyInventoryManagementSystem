import { Routes } from '@angular/router';
import { InventoryComponent } from './features/component/inventory-component/inventory-component';


export const routes: Routes = [
    {
        path: '',
        component: InventoryComponent
    },
    {
        path: 'inventory',
        component: InventoryComponent
    }
];
