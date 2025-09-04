// Basic routing shell (T041)
export const routes = [
  { path: '', loadComponent: () => import('./layout/layout.component').then(m => m.LayoutComponent) }
];