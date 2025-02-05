import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { appsettings } from '../settings/appsettings';
import { Login } from '../interfaces/Login';
import { Observable } from 'rxjs';
import { ResponseAcceso } from '../interfaces/ResponseAcceso';
import { Sesion } from '../interfaces/Sesion';

@Injectable({
  providedIn: 'root'
})
export class AccesoService {

  private http = inject(HttpClient)
  private baseUrl:string = appsettings.apiUrl;

  constructor() { }

  registrarse(objeto:Sesion):Observable<ResponseAcceso>{
    return this.http.post<ResponseAcceso>(`${this.baseUrl}Acceso/Registrarse`,objeto)
  }

  login(objeto:Login):Observable<ResponseAcceso>{
    return this.http.post<ResponseAcceso>(`${this.baseUrl}Acceso/Login`,objeto)
  }
}
