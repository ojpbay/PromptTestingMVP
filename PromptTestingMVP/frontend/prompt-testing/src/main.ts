// Required for Angular's zone-based change detection (fixes NG0908 runtime error)
import 'zone.js';
import { bootstrapApplication } from '@angular/platform-browser';
import { provideRouter } from '@angular/router';
import { provideHttpClient } from '@angular/common/http';
import { provideAnimations } from "@angular/platform-browser/animations";
import { LayoutComponent } from "./app/layout/layout.component";
import { routes } from "./app/app.routes";

bootstrapApplication(LayoutComponent, {
  providers: [provideRouter(routes), provideHttpClient(), provideAnimations()],
}).catch((err) => console.error(err));
