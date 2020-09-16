import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { environment } from '../environments/environment';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private SERVER_URL = environment.webRoot;
  constructor(private httpClient: HttpClient) { }

  getInventories() {
    return this.httpClient.get(`${this.SERVER_URL}/Inventories`);
  }

  getInventoryById(id) {
    try {
      return this.httpClient.get(`${this.SERVER_URL}/Inventories/${id}`);
    } catch (error) {
      // console.log('error console register', error.response.data);
      return error.response.data;
    }
  }

  postInventory(Inventories: any) {
    return  this.httpClient.post<any>(`${this.SERVER_URL}/Inventories`,
      Inventories).pipe(map(resp => resp));
  }

  updateInventory(Inventories: any) {
    try {
      return this.httpClient.put<any>(`${this.SERVER_URL}/Inventories`,
      Inventories).pipe(map(resp => resp));

    } catch (error) {
      // console.log('error console register', error.response.data);
      return error.response.data;
    }
  }

  deleteInventory(id) {
    try {
      return this.httpClient.delete(`${this.SERVER_URL}/Inventories/${id}`);
    } catch (error) {
      // console.log('error console register', error.response.data);
      return error.response.data;
    }
  }

}
