import { Component, inject, Input } from '@angular/core';

import {FormBuilder,FormGroup,ReactiveFormsModule} from '@angular/forms';
import {MatCardModule} from '@angular/material/card';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import { UsuarioService } from '../../services/usuario.service';
import { Router } from '@angular/router';
import { Usuario } from '../../interfaces/Usuario';
import {MatIconModule} from '@angular/material/icon';

@Component({
  selector: 'app-usuario',
  standalone: true,
  imports: [MatFormFieldModule,MatInputModule,MatButtonModule,ReactiveFormsModule,MatCardModule, MatIconModule],
  templateUrl: './usuario.component.html',
  styleUrl: './usuario.component.css'
})
export class UsuarioComponent {
  @Input('id')IdUsuario!:number;
  private UsuarioServicio = inject(UsuarioService);
  public formBuild = inject(FormBuilder);

  public formUsuarioE:FormGroup = this.formBuild.group({
    nombre: [''],
    apellidos: [''],
    cedula: [0],
    correo: [''],
    Fecha_Acceso:[''],
  })

  constructor(private router:Router){}
  //Metodo para cuando inicia 
  ngOnInit():void{
    //Validación si es edición o creación
    if(this.IdUsuario != 0){
      this.UsuarioServicio.obtener(this.IdUsuario).subscribe({
        next:(data)=>{
          this.formUsuarioE.patchValue({
            nombre: data.nombre,
            apellidos: data.apellidos,
            cedula: data.cedula,
            correo: data.correo,
            Fecha_Acceso: data.Fecha_Acceso,
          })
        },
        error:(err)=>{
          console.log(err.message)
        }
      })
    }
  }

  // Metodo para cuando termina
  guardar(){
    const objeto:Usuario={
      idUsuario: this.IdUsuario,
      nombre: this.formUsuarioE.value.nombre,
      apellidos: this.formUsuarioE.value.apellidos,
      cedula: this.formUsuarioE.value.cedula,
      correo: this.formUsuarioE.value.correo,
      Fecha_Acceso: this.formUsuarioE.value.Fecha_Acceso,
    }
    //Validación si es edición o creación
    if(this.IdUsuario == 0){
      this.UsuarioServicio.crear(objeto).subscribe({
        next:(data)=>{         
          if(Object.keys(data).length > 0){
            this.router.navigate(["/"])
          }else{
            alert("Error al crear")
          }
        },
        error:(err)=>{
          console.log(err.message)
        }
      })
    }else{
      this.UsuarioServicio.editar(this.IdUsuario,objeto).subscribe({
        next:(data)=>{
          if(this.IdUsuario > 0){
            this.router.navigate(["inicio"])
          }else{
            alert("Error al editar")
          }
        },
        error:(err)=>{
          console.log(err.message,'error155')
        }
      })
    }
  }

  volver(){
    this.router.navigate(["inicio"])
  }
}
