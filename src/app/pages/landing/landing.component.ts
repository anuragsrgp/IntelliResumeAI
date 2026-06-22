import {
  Component,
  AfterViewInit
} from '@angular/core';

import { gsap } from 'gsap';

@Component({
  selector: 'app-landing',
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.scss']
})
export class LandingComponent

implements AfterViewInit {
showDemo = false;
  ngAfterViewInit(): void {

    gsap.from('.badge',{
      y:-50,
      opacity:0,
      duration:1
    });

    gsap.from('h1',{
      y:80,
      opacity:0,
      duration:1.5
    });

    gsap.from('.left p',{
      y:80,
      opacity:0,
      duration:2
    });

    gsap.from('.ats-card',{
      x:300,
      opacity:0,
      duration:2
    });

  }

}