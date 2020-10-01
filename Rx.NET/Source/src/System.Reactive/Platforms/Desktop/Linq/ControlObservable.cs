﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT License.
// See the LICENSE file in the project root for more information. 

using System.Reactive.Concurrency;
using System.Windows.Forms;

namespace System.Reactive.Linq
{
    /// <summary>
    /// Provides a set of static methods for subscribing to IObservables using Windows Forms controls.
    /// </summary>
    public static class ControlObservable
    {
        /// <summary>
        /// Wraps the source sequence in order to run its subscription and unsubscription logic on the Windows Forms message loop associated with the specified control.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">Source sequence.</param>
        /// <param name="control">Windows Forms control whose associated message loop is used to perform subscription and unsubscription actions on.</param>
        /// <returns>The source sequence whose subscriptions and unsubscriptions happen on the Windows Forms message loop associated with the specified control.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="control"/> is null.</exception>
        /// <remarks>
        /// Only the side-effects of subscribing to the source sequence and disposing subscriptions to the source sequence are run on the specified control.
        /// In order to invoke observer callbacks on the specified control, e.g. to render results in a control, use <see cref="ObserveOn"/>.
        /// </remarks>
        public static IObservable<TSource> SubscribeOn<TSource>(this IObservable<TSource> source, Control control)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (control == null)
            {
                throw new ArgumentNullException(nameof(control));
            }

            return Synchronization.SubscribeOn(source, new ControlScheduler(control));
        }

        /// <summary>
        /// Wraps the source sequence in order to run its observer callbacks on the Windows Forms message loop associated with the specified control.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">Source sequence.</param>
        /// <param name="control">Windows Forms control whose associated message loop is used to notify observers on.</param>
        /// <returns>The source sequence whose observations happen on the Windows Forms message loop associated with the specified control.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="control"/> is null.</exception>
        public static IObservable<TSource> ObserveOn<TSource>(this IObservable<TSource> source, Control control)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (control == null)
            {
                throw new ArgumentNullException(nameof(control));
            }

            return Synchronization.ObserveOn(source, new ControlScheduler(control));
        }
    }
}
