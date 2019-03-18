
/*
Code from a student that was an infinite loop
Doesn't modify the value of the array, never hit test point or sentinal value
*/
var arr = new Array();

for(var i = 0;  arr[i] = 4; i++)
{

}



/*
My attempt at dupicating with safety
*/
alert('hello');
var arr = new Array();

for(var i = 0; arr[i]=4; i++)
{
  alert('second hello');
  console.log(i)
  arr[i] = 4;
  if(i > 5)
  {
    arr[i] = 4;
    console.log('We got to the inside test');
  }
}