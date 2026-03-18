import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { API_URL } from '../config/api';

@Injectable({
  providedIn: 'root'
})
export class Student {

  constructor(private http: HttpClient) {}

  getMyApplications() {

  const token = localStorage.getItem('token');

  return this.http.get(`${API_URL}/student/my-applications`, {
    headers: {
      Authorization: `Bearer ${token}`
    }
  });

}

getRecommendedInternships() {

  const token = localStorage.getItem('token');

  return this.http.get(`${API_URL}/student/recommended-internships`, {
    headers: {
      Authorization: `Bearer ${token}`
    }
  });

}

}
