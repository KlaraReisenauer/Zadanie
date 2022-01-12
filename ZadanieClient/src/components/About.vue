<template>
  <div class="py-8 px-6">
    <h1 class="primary--text">Employee management app</h1>
    <p>
      This application models simple employee management tool.
       It can be divided into three parts, which will be explained more closely in next paragraphs.
    </p>

    <h2>
      Employees
    </h2>
    <p>"Employees" stands for actual employees. You can perform CRUD operations on actual employees 
      of a company, as well as filter and sort results.</p>
    <p>When removing an employee, you can choose to either remove him/her permanently 
      or to archivate him/her. By archivating, the employee will be moved into "Past Employees".</p>

    <h2>
      Past Employees
    </h2>
    <p>Past employees view displays employees who were once employed by the company,
       but aren't any more. You can view, filter and sort results, but you cannot make any changes
       on them, apart from permanently removing past employee from the system.</p>

    <h2>
      Positions
    </h2>
    <p>Positions view provides list of available positions, which can an employee acquire. You can perform
      CRUD operations on them as well as filter and sort results.</p>
  </div>
</template>