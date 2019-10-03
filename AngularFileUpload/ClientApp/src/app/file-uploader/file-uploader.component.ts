import { Component, Inject } from '@angular/core';
import { HttpClient, HttpRequest, HttpEventType, HttpResponse } from '@angular/common/http'
import { FormBuilder, FormGroup, Validators, } from '@angular/forms';
import { File, User, FilesService } from '../files-service/files-service';

@Component({
    selector: 'upload-component',
    templateUrl: './file-uploader.component.html'
})

export class FileUploaderComponent {
    public progress: number;
    public message: string;
    public validationMessage: string;

    public extensionsList = ["jpg", "txt", "pdf"];
    public maxFileSizeMb = 1.5;

    public apiBaseUrl: string;
    constructor(private filesService: FilesService, private http: HttpClient, private formBuilder: FormBuilder, @Inject('BASE_URL') baseUrl: string) {
        this.apiBaseUrl = baseUrl;
    }

    upload(files) {
        if (files.length === 0)
            return;

        var file = files[0];
        const formData = new FormData();

        var fileExtension = file.name.split('.').pop();
        var fileSize = file.size;

        formData.append(file.name, file);
        this.clearMessages();

        if (!this.extensionsList.includes(fileExtension)) {
            this.validationMessage = "This file type is not available";
            return;
        }

        if (fileSize / 1024 / 1024 > this.maxFileSizeMb) {
            this.validationMessage = "Maximum file size is " + this.maxFileSizeMb + " mb";
            return;
        }

        const uploadReq = new HttpRequest('POST', this.apiBaseUrl + `upload`, formData, {
            reportProgress: true,
        });

        this.http.request(uploadReq).subscribe(event => {
            if (event.type === HttpEventType.UploadProgress)
                this.progress = Math.round(100 * event.loaded / event.total);
            else if (event.type === HttpEventType.Response) {
                this.message = event.body.toString();
                this.filesService.retrieveFilesData();
            }
        });
    }

    clearMessages() {
        this.message = "";
        this.validationMessage = "";
    }
}
