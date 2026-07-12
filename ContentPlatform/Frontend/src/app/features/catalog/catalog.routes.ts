import { Routes } from '@angular/router';

export const catalogRoutes: Routes = [
    {
        path: '',
        loadComponent: () =>
            import('./pages/catalog-page/catalog-page')
                .then(module => module.CatalogPage)
    }
];