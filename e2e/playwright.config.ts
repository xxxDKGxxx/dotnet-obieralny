import { defineConfig } from '@playwright/test';

export default defineConfig({
  testDir: './tests',
  timeout: 30_000,
  use: {
    baseURL: process.env.BASE_URL || "http://localhost:8080",
    headless: true,
    trace: 'on-first-retry',
    browserName: 'chromium',
  },
  reporter: [["list"], ["html", { outputFolder: "reports" }]],
});
