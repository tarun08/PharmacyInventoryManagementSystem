import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Medicine } from '../models/medicine.model';

import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class InventoryService {
  private readonly apiUrl = `${environment.apiUrl}/inventory`;
  private http = inject(HttpClient);


  getInventory(searchName: string = ''): Observable<Medicine[]> {
    let params = new HttpParams();
    if (searchName) {
      params = params.set('searchName', searchName);
    }
    return this.http.get<Medicine[]>(this.apiUrl, { params });
  }

  addMedicine(medicine: Medicine[]): Observable<boolean> {
    return this.http.post<boolean>(this.apiUrl, medicine);
  }


}

