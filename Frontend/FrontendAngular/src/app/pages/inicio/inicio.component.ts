import { Component, inject } from '@angular/core';

import {MatCardModule} from '@angular/material/card';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import {MatTableModule} from '@angular/material/table';
import { ReactiveFormsModule } from '@angular/forms';
import { UsuarioService } from '../../services/usuario.service';
import { Usuario } from '../../interfaces/Usuario';
import { Router } from '@angular/router';
import {MatIconModule} from '@angular/material/icon';


@Component({
  selector: 'app-inicio',
  standalone: true,
  imports: [MatCardModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatTableModule,ReactiveFormsModule,MatIconModule],
  templateUrl: './inicio.component.html',
  styleUrl: './inicio.component.css'
})
export class InicioComponent {

  private UsuarioService = inject(UsuarioService);
  public listaUsuario:Usuario[] = []; 
  public displayedColumns:string[] = ['Nombre','Apellidos','Cedula','Correo','Fecha_Acceso','Accion']
 

  obtener(){
    this.UsuarioService.lista().subscribe({
      next:(data)=>{
        if(data.length > 0){
          this.listaUsuario = data;
        }
      },
      error:(err)=>{
        console.log(err.message)
      }
    })
  }

  constructor(private router:Router){
    this.obtener();
  }

  ngOnInit(){
    this.obtener();
  }

  nuevo(){
    this.router.navigate(['usuario',0]);
  }

  editar(objeto:Usuario){
    this.router.navigate(['usuario', objeto.idUsuario]);
  }

  eliminar(objeto:Usuario){
    if(confirm("Desea eliminar el producto" + " " + objeto.nombre )){
      this.UsuarioService.eliminar(objeto.idUsuario).subscribe({
        next:(data)=>{
          if(objeto.idUsuario > 0){
            this.obtener();
          }else{
            alert("No se puede eliminar")
          }
        },
        error:(err)=>{
          console.log(err.message)
        }

      })
    }
  }
}
