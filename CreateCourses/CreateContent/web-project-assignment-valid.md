```c-lms
activity-name: Intro To HTML
activity-type: web-project
topic: First Exercise
```

## Exercise One
  
Add an H2 tag with your name and define a class named "makeItBig", assign that class to your H2 tag.

```c-lms
file-name: index.html
file-language: html
```
<html>
<head>
    <title>My Web Project</title>
    <link rel="stylesheet" type="text/css" href="styles.css">
</head>
<body>
#
</body>
</html>

```c-lms
file-name: styles.css
file-language: css
```
.makeItBig
{
#
}

```c-lms
test-id: H2SyntaxCheck
test-type: syntax
test-file: index.html 
```
```c-lms 
test: H2SyntaxCheck
test-method: ^.*<[Hh]2>.*<\/[Hh]2>.*$
test-passed: true
```
Great you figured it out (this text is optional btw)
```c-lms 
test: H2SyntaxCheck
test-method: ^.*$
test-passed: false
```
Try again!

```c-lms
test-id: CSSClassNameCheck
test-type: syntax
test-file: styles.css
```
```c-lms 
test: CSSClassNameCheck
test-method: ^.*\.makeItBig\{.*\}.*$
test-passed: true
```
Great you figured it out (this text is optional btw)
```c-lms 
test: CSSClassNameCheck
test-method: ^.*$
test-passed: false
```
Try again!