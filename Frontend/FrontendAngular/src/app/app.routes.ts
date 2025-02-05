import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { ResgistroComponent } from './pages/resgistro/resgistro.component';
import { InicioComponent } from './pages/inicio/inicio.component';
import { authGuard } from './custom/auth.guard';
import { UsuarioComponent } from './pages/usuario/usuario.component';

export const routes: Routes = [
    {path:"", component:LoginComponent},
    {path:"registro", component:ResgistroComponent},
    {path:"inicio", component:InicioComponent, canActivate:[authGuard]},
    {path:"usuario/:id", component:UsuarioComponent}
];
