import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Content } from '../models/content';

@Injectable({
    providedIn: 'root'
})
export class ContentService {
    private readonly http = inject(HttpClient);

    private readonly apiUrl =
        'http://localhost:5125/api/contents';

    getAll(): Observable<Content[]> {
        return this.http.get<Content[]>(this.apiUrl);
    }
}