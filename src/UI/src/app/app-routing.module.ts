import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home';
import { AboutComponent } from '@app/about/about.component';
import { PlayerComponent } from './player/player.component';
import { TasksComponent} from '@app/tasks/tasks.component';
import { AuthGuard } from './_helpers';

const accountModule = () => import('./account/account.module').then(x => x.AccountModule);
const usersModule = () => import('./users/users.module').then(x => x.UsersModule);

const routes: Routes = [
    { path: '', component: HomeComponent, canActivate: [AuthGuard] },
    { path: 'users', loadChildren: usersModule, canActivate: [AuthGuard] },
    { path: 'account', loadChildren: accountModule },
    {
     path: 'home',
     component: HomeComponent
   },
   {
     path: 'about',
     component: AboutComponent
   },
   {
     path: 'player',
     component: PlayerComponent
   },
  {
    path: 'tasks',
    component: TasksComponent
  },


  // otherwise redirect to home
  { path: '**', redirectTo: '' },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
