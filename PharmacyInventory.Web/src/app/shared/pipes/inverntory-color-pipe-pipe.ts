import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'inverntoryColorPipe',
  standalone: true
})
export class InverntoryColorPipePipe implements PipeTransform {
  transform(quantity: number): unknown {
    if (quantity < 10) {
      return '#e2f133ff';
    }
    return 'transparent';
  }
}
