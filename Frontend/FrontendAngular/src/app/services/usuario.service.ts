import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { appsettings } from '../settings/appsettings';
import { ResponseUsuario } from '../interfaces/ResponseUsuario';
import { Observable } from 'rxjs';
import { Usuario } from '../interfaces/Usuario';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  private http = inject(HttpClient);
  private baseUrl:string = appsettings.apiUrl + "Usuario";

  constructor() { }
  
  lista(){
    return this.http.get<Usuario[]>(this.baseUrl);
  }

  obtener(id:number){
    return this.http.get<Usuario>(`${this.baseUrl}/${id}`);
  
  }

  crear(objeto:Usuario){
    return this.http.post<Response>(this.baseUrl,objeto);
  
  }

  editar(id:number ,objeto:Usuario){
    return this.http.put<Response>(`${this.baseUrl}/${id}`,objeto);
  
  }

  eliminar(id:number){
    return this.http.delete<Response>(`${this.baseUrl}/${id}`);
  }
}
