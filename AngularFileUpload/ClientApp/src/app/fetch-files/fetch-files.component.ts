import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject, BehaviorSubject, Observable } from 'rxjs';
import { Group, File, User, FilesService } from '../files-service/files-service';

@Component({
    selector: 'app-fetch-files',
  templateUrl: './fetch-files.component.html'
})
export class FetchFilesComponent {
    public fileGroups: Observable<Group[]>;

    constructor(private filesService: FilesService, http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.fileGroups = this.filesService._files;
        this.filesService.retrieveFilesData();
    }
}
