import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
	name: 'expiryColor',
	standalone: true
})
export class ExpiryColorPipe implements PipeTransform {
	transform(expiryDate: string | Date): unknown {
		const expiry = new Date(expiryDate);
		const today = new Date();

		const diffTime = expiry.getTime() - today.getTime();
		const diffDays = diffTime / (1000 * 3600 * 24);

		if (diffDays < 30) {
			return '#f95e5eff';
		}
		return 'transparent';
	}
}
