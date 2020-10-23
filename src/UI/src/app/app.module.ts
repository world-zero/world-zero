import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { PlayerComponent } from './player/player.component';
import { MDBBootstrapModule } from 'angular-bootstrap-md';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

/* import { MatToolbarModule } from '@angular/material/toolbar';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatDividerModule } from '@angular/material/divider';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatSelectModule } from '@angular/material/select';
*/

// used to create fake backend
import { fakeBackendProvider } from './_helpers';

import { AppRoutingModule } from './app-routing.module';
import { JwtInterceptor, ErrorInterceptor } from './_helpers';
import { AppComponent } from './app.component';
import { AlertComponent } from './_components';
import { HomeComponent } from './home';
import { AboutComponent } from './about/about.component';
import { TasksComponent } from './tasks/tasks.component';
import { FactionComponent } from './faction/faction.component';
import { EditPraxisComponent } from './edit-praxis/edit-praxis.component';
import { CKEditorModule } from 'ckeditor4-angular';

@NgModule({
    imports: [
        BrowserModule,
        ReactiveFormsModule,
        HttpClientModule,
        AppRoutingModule,
        CKEditorModule,
        MDBBootstrapModule.forRoot()
    ],
    declarations: [
        AppComponent,
        AlertComponent,
        HomeComponent,
        AboutComponent ,
        TasksComponent ,
        FactionComponent ,
        EditPraxisComponent ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
        // provider used to create fake backend
        fakeBackendProvider
    ],
    bootstrap: [AppComponent]
})

export class AppModule { }
