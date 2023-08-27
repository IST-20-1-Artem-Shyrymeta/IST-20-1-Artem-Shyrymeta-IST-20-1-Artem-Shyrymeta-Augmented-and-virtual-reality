/*
 * Copyright (c) 2018 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * Notwithstanding the foregoing, you may not use, copy, modify, merge, publish, 
 * distribute, sublicense, create a derivative work, and/or sell copies of the 
 * Software in any work that is designed, intended, or marketed for pedagogical or 
 * instructional purposes related to programming, coding, application development, 
 * or information technology.  Permission for such use, copying, modification,
 * merger, publication, distribution, sublicensing, creation of derivative works, 
 * or sale is expressly withheld.
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using UnityEngine.UI;
using UnityEngine;

public class Chef : MonoBehaviour
{
    public Text speechBubble;

    public void OrderCreated()
    {
        Order order = GameManager.instance.currentOrder;
        if (IsTheWorks(order))
        {
            speechBubble.text = "Order up! \nSomeone has ordered The Works!";
        }
        else if (IsSweetAndSpicy(order))
        {
            speechBubble.text = "Order up! \nIt's a sweet and spicy one!";
        }
        else if (IsCapricciosa(order))
        {
            speechBubble.text = "Order up! \nCapricciosa! Sbrigati!";
        }
        else if (IsVegetarian(order))
        {
            speechBubble.text = "Order up! \nUno Vegetariano!";
        }
        else if (IsPepperoni(order))
        {
            speechBubble.text = "Okay, a Pepperoni Passion for this one.";
        }
        else if (IsMushroom(order))
        {
            speechBubble.text = "This one is a Funghi!";
        }
        else if (IsPineapple(order))
        {
            speechBubble.text = "A sweet, juicy Pineapple Pizza!";
        }
        else if (IsMargherita(order))
        {
            speechBubble.text = "Ah! A nice napoletana for this customer!";
        }
    }

    private bool IsCapricciosa(Order order)
    {
        return order.mushroom && order.pepperoni && !order.pineapple;
    }

    private bool IsVegetarian(Order order)
    {
        return order.mushroom && order.pineapple && !order.pepperoni;
    }

    private bool IsMushroom(Order order)
    {
        return order.mushroom && !order.pepperoni && !order.pineapple;
    }

    private bool IsPineapple(Order order)
    {
        return order.pineapple && !order.pepperoni && !order.mushroom;
    }

    private bool IsPepperoni(Order order)
    {
        return order.pepperoni && !order.mushroom && !order.pineapple;
    }

    private bool IsSweetAndSpicy(Order order)
    {
        return order.pineapple && order.pepperoni && !order.mushroom;
    }

    private bool IsTheWorks(Order order)
    {
        return order.mushroom && order.pineapple && order.pepperoni;
    }

    private bool IsMargherita(Order order)
    {
        return !order.pineapple && !order.pepperoni && !order.mushroom;
    }
}
