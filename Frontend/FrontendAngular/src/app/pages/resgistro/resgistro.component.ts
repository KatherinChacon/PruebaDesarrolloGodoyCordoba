import { Component, inject } from '@angular/core';

import {MatCardModule} from '@angular/material/card';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AccesoService } from '../../services/acceso.service';
import { Router } from '@angular/router';
import { Sesion } from '../../interfaces/Sesion';

@Component({
  selector: 'app-resgistro',
  standalone: true,
  imports: [MatCardModule, MatFormFieldModule, MatInputModule, MatButtonModule, ReactiveFormsModule],
  templateUrl: './resgistro.component.html',
  styleUrl: './resgistro.component.css'
})
export class ResgistroComponent {

  private accesoService = inject(AccesoService);
  private router = inject(Router);
  public formBuild = inject(FormBuilder);

  public formRegistro: FormGroup = this.formBuild.group({
    correo:["",Validators.required],
    contrasena:["",Validators.required]
  })

  registrase(){
    if(this.formRegistro.invalid) return;

    const objeto:Sesion = {
      correo: this.formRegistro.value.correo,
      contrasena: this.formRegistro.value.contrasena
    }

    this.accesoService.registrarse(objeto).subscribe({
      next:(data) =>{
        if(data.isSuccess){
          this.router.navigate([''])
        }else{
          alert("No se pudo registrar")          
        }
      },
      error:(error) => {
        console.log(error.message);
      }
    })
  }
  
  volver(){
    this.router.navigate([''])
  }

}
