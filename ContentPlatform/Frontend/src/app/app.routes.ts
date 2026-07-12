import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: '',
        redirectTo: 'catalog',
        pathMatch: 'full'
    },
    {
        path: 'catalog',
        loadChildren: () =>
            import('./features/catalog/catalog.routes')
                .then(module => module.catalogRoutes)
    },
    {
        path: '**',
        redirectTo: 'catalog'
    }
];