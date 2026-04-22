import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Medicine } from '../models/medicine.model';

@Injectable({
  providedIn: 'root',
})
export class InventoryService {
  private readonly apiUrl = '/api/inventory';
  private http = inject(HttpClient);

  getInventory(searchName: string = ''): Observable<Medicine[]> {
    let params = new HttpParams();
    if (searchName) {
      params = params.set('searchName', searchName);
    }
    return this.http.get<Medicine[]>(this.apiUrl, { params });
  }

}

