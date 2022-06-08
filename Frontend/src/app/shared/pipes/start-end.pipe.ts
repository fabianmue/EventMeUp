import { DatePipe } from '@angular/common';
import { Inject, LOCALE_ID, Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'startEnd' })
export class StartEndPipe implements PipeTransform {
  constructor(@Inject(LOCALE_ID) private readonly locale: string) {}

  transform(value: { start?: string; end?: string | null }): string | null {
    const datePipe = new DatePipe(this.locale);
    const start = new Date(value.start!);
    if (value.end === null) {
      const format =
        start.getFullYear() === new Date().getFullYear()
          ? 'EEE, d. MMM H:mm'
          : 'EEE, d. MMM, y H:mm';
      return datePipe.transform(start, format);
    }

    const end = new Date(value.end!);
    if (
      start.getDay() === end.getDay() &&
      start.getDate() === end.getDate() &&
      start.getFullYear() === end.getFullYear()
    ) {
      const format =
        start.getFullYear() === new Date().getFullYear()
          ? 'EEE, d. MMM H:mm'
          : 'EEE, d. MMM, y H:mm';
      return `${datePipe.transform(start, format)} - ${datePipe.transform(
        end,
        'h:mm'
      )}`;
    }

    if (start.getFullYear() === end.getFullYear()) {
      const format =
        start.getFullYear() === new Date().getFullYear()
          ? 'EEE, d. MMM H:mm'
          : 'EEE, d. MMM, y H:mm';
      return `${datePipe.transform(start, format)} - ${datePipe.transform(
        end,
        format
      )}`;
    }

    const format = 'EEE, d. MMM, y H:mm';
    return `${datePipe.transform(start, format)} - ${datePipe.transform(
      end,
      format
    )}`;
  }
}
