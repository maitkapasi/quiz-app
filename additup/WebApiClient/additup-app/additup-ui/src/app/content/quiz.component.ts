import { Component, OnInit } from '@angular/core';
import { QuizDataService } from 'src/app/services/quiz-data.service';
import { finalize } from '../../../node_modules/rxjs/operators';
import { Session, Exercise, Answer } from 'src/app/datamodels/models';
import { ToastrService } from 'ngx-toastr';
import { ViewChild } from '@angular/core';
import { ElementRef } from '@angular/core';
import { Helper } from '../../../../../common/src'

@Component({
  selector: 'app-quiz',
  templateUrl: './quiz.component.html',
  styles: []
})
export class QuizComponent {

  loading: boolean = false;
  session: Session;
  exercise: Exercise;
  timer: any;
  remainingTime: number;
  answerToSubmit: string;
  helper: Helper = new Helper();
  @ViewChild('answerToSubmitElement') answerToSubmitElement: ElementRef;
  

  constructor(private quizDataService: QuizDataService, private toastr: ToastrService) { 
    this.startSession();
  }

  startSession() {
    
    this.loading = true;
    
    this.quizDataService.createSession()
    .pipe(finalize(() => this.loading = false))
    .subscribe((createdSession) => {
    
      this.session = createdSession;
      this.getQuiz();
      
    })
  }

  getQuiz() {
    
    this.loading = true;
    
    this.quizDataService.getExercise(this.session.id)
    .pipe(finalize(() => this.loading = false))
    .subscribe((createdExercise) => {
    
      this.exercise = createdExercise;
      this.answerToSubmit = "";
      if(this.answerToSubmitElement) this.answerToSubmitElement.nativeElement.focus();
      this.startTimer();
    
    })
  }

  startTimer() {

    this.stopTimerIfRunning();

    this.remainingTime = this.exercise.timeLimit;

    this.timer = setInterval(() => {

      this.remainingTime--;
      if (this.remainingTime <= 0) {
        if (this.timer) clearInterval(this.timer);
        this.timeOver();
      }
    }, 1000);
  }

  stopTimerIfRunning() {
    if (this.timer) {
      clearInterval(this.timer);
      this.timer = null;
    } 
    
  }

  timeOver() {
    this.stopTimerIfRunning();
    this.toastr.error(`Final Rank: ${this.session.rank}, Level: ${this.session.level}`, "Oops, Time Over!", {timeOut: 3000});
      
    this.endSession();
  }

  submitAnswer() {

    if(!this.helper.isInteger(this.answerToSubmit)) {
      this.toastr.warning("Please provide integer value.", "Validaiton Error", {timeOut: 3000}); 
      return;
    }
    
    this.loading = true;

    this.stopTimerIfRunning();

    let answer: Answer = {exerciseId: this.exercise.id, sessionId: this.session.id, submittedAnswer: this.answerToSubmit };

    this.quizDataService.postAnswer(this.session.id, answer)
    .pipe(finalize(() => this.loading = false))
    .subscribe((result) => {
      if(result.answerCorrect) {
        if(result.allLevelCompleted) {
          this.toastr.success("All stages completed.", "Correct!", {timeOut: 3000});
          this.endSession();
        } else {
          this.session.level = result.level;
          this.session.rank = result.rank;
          this.toastr.success(`Next Rank: ${this.session.rank}, Level: ${this.session.level}`, "Correct!", {timeOut: 1500});
          this.getQuiz();
        }
      } else {
        this.toastr.error(`Final Rank: ${this.session.rank}, Level: ${this.session.level}`, "Oops, Wrong Answer!", {timeOut: 3000});
        this.endSession();
      }
    })
  }

  endSession() {
    this.session = null;
    this.exercise = null;
    this.stopTimerIfRunning();
  }

}
