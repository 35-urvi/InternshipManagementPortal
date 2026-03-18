// import { Injectable } from '@angular/core';
// import { HttpClient } from '@angular/common/http';
// import { API_URL } from '../config/api';
// import { Observable } from 'rxjs';

// @Injectable({
//   providedIn: 'root'
// })
// export class Internship {

//   constructor(private http: HttpClient) {}

//   getAll(): Observable<any> {
//     return this.http.get(`${API_URL}/internship/all`);
//   }

// }
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { API_URL } from '../config/api';

@Injectable({
  providedIn: 'root'
})
export class InternshipService {

  constructor(private http: HttpClient) {}

  getAllInternships() {

    return this.http.get(`${API_URL}/internship/all`);

  }

  getInternship(id: number) {

    return this.http.get(`${API_URL}/internship/${id}`);

  }

  createInternship(data: any) {

    const token = localStorage.getItem('token');

    return this.http.post(`${API_URL}/internship/create`, data, {
      headers: {
        Authorization: `Bearer ${token}`
      }
    });

  }

  // searchInternships(keyword?: string, skill?: string, minStipend?: number) {

  //   return this.http.get(`${API_URL}/internship/search`, {
  //     params: {
  //       keyword: keyword || '',
  //       skill: skill || '',
  //       minStipend: minStipend ? minStipend.toString() : ''
  //     }
  //   });

  // }
    searchInternships(keyword?: string, skill?: string, minStipend?: number) {

  let params: any = {};

  if (keyword) params.keyword = keyword;
  if (skill) params.skill = skill;
  if (minStipend) params.minStipend = minStipend;

  return this.http.get(`${API_URL}/internship/search`, { params });

}

updateInternship(id: number, data: any) {

  const token = localStorage.getItem('token');

  return this.http.put(`${API_URL}/internship/update/${id}`, data, {
    headers: {
      Authorization: `Bearer ${token}`
    }
  });

}

deleteInternship(id: number) {

  const token = localStorage.getItem('token');

  return this.http.delete(`${API_URL}/internship/delete/${id}`, {
    headers: {
      Authorization: `Bearer ${token}`
    }
  });

}


}

