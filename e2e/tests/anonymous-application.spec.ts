import { test, expect } from '@playwright/test';

test('searching and creating an application by anonymous user', async ({ page }) => {
  await page.goto('http://localhost:8080/');
  await page.getByRole('spinbutton', { name: 'Kwota (PLN)' }).fill('20000');
  await page.getByRole('spinbutton', { name: 'Okres spłaty (Miesiące)' }).fill('12');
  await page.getByRole('button', { name: 'Szukaj ofert' }).click();
  await page.getByRole('spinbutton', { name: 'Miesięczny dochód (PLN)' }).fill('10000');
  await page.getByRole('spinbutton', { name: 'Miesięczne koszty (PLN)' }).fill('6000');
  await page.getByRole('spinbutton', { name: 'Wiek' }).fill('22');
  await page.getByRole('spinbutton', { name: 'Osoby na utrzymaniu' }).fill('2');
  await page.getByRole('button', { name: 'Szukaj ofert' }).click();
  await page.waitForTimeout(1000);
  await page.locator('mat-card-content').filter({ hasText: /ArdalisBank/ }).first().getByRole('button').click();
  await page.getByRole('textbox', { name: 'Imię' }).fill('John');
  await page.getByRole('textbox', { name: 'Nazwisko' }).fill('Doe');
  await page.getByRole('textbox', { name: 'E-mail' }).fill('john.doe@mail.com');
  await page.getByRole('textbox', { name: 'Adres' }).fill('ul. Marszałkowska 125/127 01-000 Warszawa');
  await page.getByRole('textbox', { name: 'Nr telefonu' }).fill('123456789');
  await page.getByRole('textbox', { name: 'Zawód' }).fill('prawnik');
  await page.getByRole('button', { name: 'Aplikuj' }).click();
  await page.waitForSelector('.cdk-overlay-pane');
  const successOverlay = page.locator('.cdk-overlay-pane')
    .filter({ hasText: 'Pomyślnie utworzono aplikację!' });
  await expect(successOverlay).toBeVisible();
});