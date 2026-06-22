import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ResumeService } from '../../services/resume.service';

@Component({
  selector: 'app-upload-resume',
  templateUrl: './upload-resume.component.html',
  styleUrls: ['./upload-resume.component.scss']
})
export class UploadResumeComponent {

  selectedFile!: File;

  constructor(
    private resumeService: ResumeService,
    private router: Router
  ) {}

  onFileSelected(event: any) {

    this.selectedFile =
      event.target.files[0];
  }

  uploadResume() {

    if (!this.selectedFile) {

      alert(
        'Please Select Resume'
      );

      return;
    }

    this.resumeService
      .uploadResume(
        this.selectedFile
      )
      .subscribe({

        next: (uploadRes: any) => {

          console.log(
            'UPLOAD RESPONSE',
            uploadRes
          );

          this.resumeService
            .analyzeResume(
              uploadRes.resumeText
            )
            .subscribe({

              next: (analysis: any) => {

                console.log(
                  'AI ANALYSIS',
                  analysis
                );

                localStorage.setItem(
                  'analysis',
                  JSON.stringify(
                    analysis
                  )
                );

                this.router.navigate([
                  '/result'
                ]);
              },

              error: (err: any) => {

                console.error(
                  'AI ERROR',
                  err
                );

                alert(
                  'AI Analysis Failed'
                );
              }

            });
        },

        error: (err: any) => {

          console.error(
            'UPLOAD ERROR',
            err
          );

          alert(
            'Upload Failed'
          );
        }

      });
  }
}