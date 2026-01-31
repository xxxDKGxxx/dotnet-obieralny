import { test, expect } from '@playwright/test';

test('mock log in, should be able make an application from search, withdraw, fill data in profile, make application from autofill', async ({ page }) => {
  await page.goto('http://localhost:8080/');
  const apiResponse = await page.request.post('http://localhost:5000/api/v1/auth/test-user', {
    data: { 
      Email: 'test.user@example.com' 
    },
    headers: {
      'Content-Type': 'application/json'
    }
  });
  expect(apiResponse.status()).toBe(200);
  const responseBody = await apiResponse.json();
  const authToken = responseBody.accessToken;
  if (!authToken) {
    throw new Error('No token found in login response');
  }
  await page.addInitScript((token) => {
  globalThis.localStorage?.setItem('auth_token', token);
  }, authToken);
  await page.goto('http://localhost:8080/');
  await page.getByRole('spinbutton', { name: 'Kwota (PLN)' }).fill('90000');
  await page.getByRole('spinbutton', { name: 'Okres spłaty (Miesiące)' }).fill('48');
  await page.getByRole('button', { name: 'Szukaj ofert' }).click();
  await page.getByRole('spinbutton', { name: 'Miesięczny dochód (PLN)' }).fill('12000');
  await page.getByRole('spinbutton', { name: 'Miesięczne koszty (PLN)' }).fill('8000');
  await page.getByRole('spinbutton', { name: 'Wiek' }).fill('25');
  await page.getByRole('spinbutton', { name: 'Osoby na utrzymaniu' }).fill('3');
  await page.getByRole('button', { name: 'Szukaj ofert' }).click();
  await page.waitForTimeout(1000);
  await page.getByRole('button', { name: 'Szczegóły...' }).first().click();
  await page.getByRole('textbox', { name: 'Adres' }).fill('ul. Marszałkowska 2137, 69-420 Warszawa');
  await page.getByRole('textbox', { name: 'Nr telefonu' }).fill('541541541');
  await page.getByRole('textbox', { name: 'Zawód' }).fill('malarz');
  await page.getByRole('button', { name: 'Aplikuj' }).click();
  await page.getByRole('cell', { name: '90000' }).last().click();
  await expect(page.getByText('Utworzona').last()).toBeVisible();
  await page.getByRole('button', { name: 'Zrezygnuj z aplikacji' }).click();
  await page.getByRole('button', { name: 'Potwierdź' }).click();
  await expect(page.getByText('Wycofana')).toBeVisible();
  await page.getByRole('button', { name: 'Test Mock' }).click();
  await page.getByRole('menuitem', { name: 'Moje konto' }).click();
  await page.getByRole('textbox', { name: 'Adres zamieszkania' }).fill('ul. Emilii Plater 69 21-370 Warszawa');
  await page.getByRole('textbox', { name: 'Numer telefonu' }).fill('420420420');
  await page.getByRole('textbox', { name: 'Zawód' }).fill('kamieniarz');
  await page.getByRole('spinbutton', { name: 'Miesięczny dochód (PLN)' }).fill('15000');
  await page.getByRole('spinbutton', { name: 'Miesięczne koszty (PLN)' }).fill('8500');
  await page.getByRole('spinbutton', { name: 'Wiek' }).fill('23');
  await page.getByRole('spinbutton', { name: 'Osoby na utrzymaniu' }).fill('2');
  await page.getByRole('button', { name: 'Zapisz zmiany' }).click();
  await expect(page.getByText('Profil zaktualizowany pomyślnie')).toBeVisible();
  await page.getByRole('button', { name: 'OK' }).click();
  await page.getByRole('link', { name: 'Wyszukiwarka' }).click();
  await page.getByRole('spinbutton', { name: 'Kwota (PLN)' }).fill('50000');
  await page.getByRole('spinbutton', { name: 'Okres spłaty (Miesiące)' }).fill('36');
  await page.locator('mat-card-content').filter({ hasText: /ArdalisBank/ }).first().getByRole('button').click();
  await page.getByRole('button', { name: 'Aplikuj' }).click();
  await expect(page.getByRole('cell', { name: 'Utworzona' }).last()).toBeVisible();
  page.getByRole('cell', { name: 'Utworzona' }).last().click();
  await page.getByRole('button', { name: 'Zrezygnuj z aplikacji' }).click();
  await page.getByRole('button', { name: 'Potwierdź' }).click();
});