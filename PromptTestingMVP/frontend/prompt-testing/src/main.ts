import { bootstrapApplication } from '@angular/platform-browser';
import { provideRouter } from '@angular/router';
import { provideHttpClient } from '@angular/common/http';
import { LayoutComponent } from './app/layout/layout.component';
import { routes } from './app/app.routes';

bootstrapApplication(LayoutComponent, {
  providers: [
    provideRouter(routes),
    provideHttpClient(),
  ]
}).catch(err => console.error(err));
