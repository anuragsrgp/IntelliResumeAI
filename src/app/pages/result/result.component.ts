import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-result',
  templateUrl: './result.component.html',
  styleUrls: ['./result.component.scss']
})
export class ResultComponent implements OnInit {

  result: any = {
    isResume: true,
    profile: '',
    experienceLevel: '',
    atsScore: 0,
    profileSummary: '',
    skills: [],
    missingSkills: [],
    strengths: [],
    weaknesses: [],
    suggestions: [],
    learningRoadmap: [],
    interviewQuestions: []
  };

  ngOnInit(): void {

    const analysisData =
      localStorage.getItem(
        'analysis'
      );

    if (analysisData) {

      this.result =
        JSON.parse(
          analysisData
        );

      console.log(
        this.result
      );
    }
  }
}