import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { API_URL } from '../config/api';

@Injectable({
  providedIn: 'root'
})
export class Company {

  constructor(private http: HttpClient) {}

//   getMyInternships(companyId: number) {

//     const token = localStorage.getItem('token');

//     return this.http.get(`${API_URL}/company/my-internships?companyId=${companyId}`, {
//       headers: {
//         Authorization: `Bearer ${token}`
//       }
//     });

//   }
    getMyInternships() {

    const token = localStorage.getItem('token');

    return this.http.get(`${API_URL}/company/my-internships`, {
        headers: {
        Authorization: `Bearer ${token}`
        }
    });

    }


  getApplications(internshipId: number) {

    const token = localStorage.getItem('token');

    return this.http.get(`${API_URL}/company/applications/${internshipId}`, {
      headers: {
        Authorization: `Bearer ${token}`
      }
    });

  }

  acceptApplication(id: number) {

    const token = localStorage.getItem('token');

    return this.http.put(`${API_URL}/company/application/accept/${id}`, {}, {
      headers: {
        Authorization: `Bearer ${token}`
      }
    });

  }

  rejectApplication(id: number) {

    const token = localStorage.getItem('token');

    return this.http.put(`${API_URL}/company/application/reject/${id}`, {}, {
      headers: {
        Authorization: `Bearer ${token}`
      }
    });

  }

}
