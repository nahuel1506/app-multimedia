import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: '',
        loadChildren: () =>
            import('./features/home/home.routes')
                .then(module => module.homeRoutes)
    },
    {
        path: 'catalog',
        loadChildren: () =>
            import('./features/catalog/catalog.routes')
                .then(module => module.catalogRoutes)
    },
    {
        path: '**',
        redirectTo: ''
    }
];
