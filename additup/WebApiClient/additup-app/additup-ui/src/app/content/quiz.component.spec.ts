import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QuizComponent } from './quiz.component';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { ApiHandlerService } from 'src/app/services/api-handler.service';
import { QuizDataService } from 'src/app/services/quiz-data.service';
import { Observable, of } from 'rxjs';
import { Session, Exercise, Result, RankEnum } from 'src/app/datamodels/models';
import { Answer } from 'src/app/datamodels/Answer';
import { fakeAsync } from '@angular/core/testing';
import { tick } from '@angular/core/testing';
import { discardPeriodicTasks } from '@angular/core/testing';
import { DebugElement } from '@angular/core';
import { By } from '@angular/platform-browser';

class MockQuizDataService {
  createSession(): Observable<Session> {

    let session: Session = { id: "s1", level: 1, rank: RankEnum.Beginner };
    return of(session);
  }

  getExercise(sessionId: string): Observable<Exercise> {
    let exercise: Exercise = { id: "e1", expression1: 3, expression2: 5, timeLimit: 30, assignedSession: "s1", createdDateTime: new Date().toString() };
    return of(exercise);
  }

  postAnswer(sessionId: string, answer: Answer): Observable<Result> {
    let result : Result = {answerCorrect: true, rank: RankEnum.Beginner, level: 2, allLevelCompleted: false };
    return of(result);
  }
}

describe('QuizComponent', () => {
  let component: QuizComponent;
  let fixture: ComponentFixture<QuizComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QuizComponent ],
      imports: [
        BrowserModule,
        FormsModule,
        CommonModule,
        BrowserAnimationsModule,
        ToastrModule.forRoot(),
      ],
      providers: [
        ApiHandlerService, 
        {provide: QuizDataService, useClass: MockQuizDataService}
      ],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QuizComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create quiz component', () => {
    expect(component).toBeTruthy();
  });

  it('should start a new session and get an exercise on load', () => {
    // Assert
    expect(component.session).toBeTruthy();
    expect(component.exercise).toBeTruthy();
  });

  it('should disable submit button if answer is empty', () => {
    // Arrange
    const de: DebugElement = fixture.debugElement;
    const submitButton = de.query(By.css('#submitButton'));
    
    // Act
    component.answerToSubmit = "";
    fixture.detectChanges();

    // Assert
    expect(submitButton.properties.disabled).toEqual(true);

  });

  it('should enable submit button if answer is not empty', () => {
    // Arrange
    const de: DebugElement = fixture.debugElement;
    const submitButton = de.query(By.css('#submitButton'));
    
    // Act
    component.answerToSubmit = "123";
    fixture.detectChanges();

    // Assert
    expect(submitButton.properties.disabled).toEqual(false);

  });

  it('should end quiz if time limit passes max time limit ', fakeAsync((): void => {

    // Arrange 
    component.startTimer();

    // Act
    tick(35000);

    // Assert 
    expect(component.remainingTime).toEqual(0);
    expect(component.session).toBeFalsy();
    expect(component.exercise).toBeFalsy();
    expect(component.timer).toBeFalsy();
    discardPeriodicTasks();
  }));     

  it('should get a new quiz on submitting correct answer ', (): void => {
    // Arrange
    component.answerToSubmit = "8"; //does not matter which value, we are going to use stubbed response :) 

    let quizDataService = TestBed.get(QuizDataService, null);

    let newExercise: Exercise = { id: "e2", expression1: 4, expression2: 2, timeLimit: 30, assignedSession: "s1", createdDateTime: new Date().toString() };
    spyOn(quizDataService, 'getExercise').and.returnValue(of(newExercise));

    // Act
    component.submitAnswer();

    // Assert
    expect(quizDataService.getExercise).toHaveBeenCalled();
    expect(component.exercise).toBeTruthy();
    expect(component.exercise).toEqual(newExercise);
    expect(component.session).toBeTruthy();
  });

  it('should end the quiz on submitting incorrect answer ', (): void => {
    // Arrange
    component.answerToSubmit = "8"; //does not matter which value, we are going to use stubbed response :) 

    let quizDataService = TestBed.get(QuizDataService, null);

    let failureResult : Result = {answerCorrect: false, rank: RankEnum.Beginner, level: 1, allLevelCompleted: false };
    spyOn(quizDataService, 'postAnswer').and.returnValue(of(failureResult));

    let newExercise: Exercise = { id: "e2", expression1: 4, expression2: 2, timeLimit: 30, assignedSession: "s1", createdDateTime: new Date().toString() };
    spyOn(quizDataService, 'getExercise').and.returnValue(of(newExercise));

    // Act
    component.submitAnswer();

    // Assert
    expect(component.exercise).toBeFalsy();
    expect(component.session).toBeFalsy();
    expect(quizDataService.postAnswer).toHaveBeenCalled();
    expect(quizDataService.getExercise).not.toHaveBeenCalled();
  });  

  it('should end the quiz on submitting final correct answer ', (): void => {
    // Arrange
    component.answerToSubmit = "8"; //does not matter which value, we are going to use stubbed response :) 
    let quizDataService = TestBed.get(QuizDataService, null);
    
    let failureResult : Result = {answerCorrect: true, rank: RankEnum.Expert, level: 3, allLevelCompleted: true };
    spyOn(quizDataService, 'postAnswer').and.returnValue(of(failureResult));

    let newExercise: Exercise = { id: "e2", expression1: 4, expression2: 2, timeLimit: 30, assignedSession: "s1", createdDateTime: new Date().toString() };
    spyOn(quizDataService, 'getExercise').and.returnValue(of(newExercise));

    // Act
    component.submitAnswer();

    // Assert
    expect(component.exercise).toBeFalsy();
    expect(component.session).toBeFalsy();
    expect(quizDataService.postAnswer).toHaveBeenCalled();
  });    

  it('should restart a new quiz session', (): void => {
    // Arrange
    component.endSession();
    
    // Act
    component.startSession();

    // Assert
    expect(component.session).toBeTruthy();
    expect(component.exercise).toBeTruthy();
  });      

});
